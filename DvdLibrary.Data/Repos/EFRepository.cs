using DvdLibrary.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibrary.Models;
using System.Data.SqlClient;

namespace DvdLibrary.Data.Repos
{
    public class EFRepository : IDvdRepository
    {
        List<Dvds> _listOfDvds = new List<Dvds>();

        public EFRepository()
        {
            GetAll();
        }

        public List<Dvds> GetAll()
        {
            _listOfDvds.Clear();

            using (var ctx = new DvdLibraryEntities())
            {
                var allDvds = ctx.Dvds;

                foreach (var dvd in allDvds)
                {
                    _listOfDvds.Add(dvd);
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
            using (var ctx = new DvdLibraryEntities())
            {
                ctx.Dvds.Add(dvd);
                ctx.SaveChanges();
            }
        }

        public void UpdateDvd(Dvds dvd)
        {
            using (var ctx = new DvdLibraryEntities())
            {
                var updateThis = ctx.Dvds.SingleOrDefault(d => d.DvdId == dvd.DvdId);

                updateThis.Title = dvd.Title;
                updateThis.ReleaseYear = dvd.ReleaseYear;
                updateThis.Director = dvd.Director;
                updateThis.Rating = dvd.Rating;
                updateThis.Notes = dvd.Notes;
                ctx.SaveChanges();
            }
        }

        public void DeleteDvd(int dvdId)
        {
            using (var ctx = new DvdLibraryEntities())
            {
                var deleteThis = ctx.Dvds.SingleOrDefault(d => d.DvdId == dvdId);

                ctx.Dvds.Remove(deleteThis);
                ctx.SaveChanges();
            }
        }
    }
}
