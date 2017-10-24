using DvdLibrary.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibrary.Models;
using System.Data.SqlClient;
using System.Data;

namespace DvdLibrary.Data.Repos
{
    public class ADORepository : IDvdRepository
    {
        List<Dvds> _listOfDvds = new List<Dvds>();

        public ADORepository()
        {
            GetAll();
        }

        public List<Dvds> GetAll()
        {
            _listOfDvds.Clear();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SelectAllDvds", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvds currentRow = new Dvds();
                        currentRow.DvdId = (int)dr["DvdId"];
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.ReleaseYear = (int)dr["ReleaseYear"];
                        currentRow.Director = dr["Director"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();
                        currentRow.Notes = dr["Notes"].ToString();

                        _listOfDvds.Add(currentRow);
                    }
                }
            }
            return _listOfDvds;
        }

        public List<Dvds> LoadListByRating(string rating)
        {
            var listByTitle = _listOfDvds.Where(t => t.Rating == rating);
            return listByTitle.ToList();
        }

        public List<Dvds> LoadListByDirector(string director)
        {
            var listByTitle = _listOfDvds.Where(t => t.Director == director);
            return listByTitle.ToList();
        }

        public List<Dvds> LoadListByReleaseyear(string releaseYear)
        {
            int.TryParse(releaseYear, out int year);

            var listByTitle = _listOfDvds.Where(t => t.ReleaseYear == year);
            return listByTitle.ToList();
        }

        public List<Dvds> LoadListByTitle(string title)
        {
            var listByTitle = _listOfDvds.Where(t => t.Title == title);
            return listByTitle.ToList();
        }

        public Dvds LoadSingleDvdDetail(int dvdId)
        {
            var dvd = _listOfDvds.SingleOrDefault(d => d.DvdId == dvdId);
            return dvd;
        }

        public void SaveDvd(Dvds dvd)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@DvdId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvd.ReleaseYear);
                cmd.Parameters.AddWithValue("@Director", dvd.Director);
                cmd.Parameters.AddWithValue("@Rating", dvd.Rating);
                cmd.Parameters.AddWithValue("@Notes", dvd.Notes);
         
                cn.Open();

                cmd.ExecuteNonQuery();

                dvd.DvdId = (int)param.Value;
            }
        }

        public void UpdateDvd(Dvds dvd)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvd.DvdId);
                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvd.ReleaseYear);
                cmd.Parameters.AddWithValue("@Director", dvd.Director);
                cmd.Parameters.AddWithValue("@Rating", dvd.Rating);
                cmd.Parameters.AddWithValue("@Notes", dvd.Notes);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteDvd(int dvdId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
     
                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
