using DvdLibrary.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibrary.Models;

namespace DvdLibrary.Data.Repos
{
    public class MockData : IDvdRepository
    {
        static List<Dvds> _listOfDvds = new List<Dvds>
        {
            new Dvds
            {
                DvdId = 1,
                Title = "Lion King",
                ReleaseYear = 1992,
                Director = "Judy Thao",
                Rating = "G",
                Notes = "Simba learns to be king."
            },

            new Dvds
            {
                DvdId = 2,
                Title = "Mulan",
                ReleaseYear = 1998,
                Director = "Steven Spielburg",
                Rating = "PG",
                Notes = "Awesome woman warrior"
            }
        };

        public List<Dvds> GetAll()
        {
            return _listOfDvds;
        }

        public Dvds LoadSingleDvdDetail(int dvdId)
        {
            var dvd = _listOfDvds.SingleOrDefault(d => d.DvdId == dvdId);
            return dvd;
        }

        public List<Dvds> LoadListByTitle(string title)
        {
            var listByTitle = _listOfDvds.Where(t => t.Title == title);
            return listByTitle.ToList();
        }

        public List<Dvds> LoadListByReleaseyear(string releaseyear)
        {
            int.TryParse(releaseyear, out int year);

            var listByYear = _listOfDvds.Where(t => t.ReleaseYear == year);
            return listByYear.ToList();
        }

        public List<Dvds> LoadListByDirector(string director)
        {
            var listByDirector = _listOfDvds.Where(t => t.Director == director);
            return listByDirector.ToList();
        }

        public List<Dvds> LoadListByRating(string rating)
        {
            var listByRating = _listOfDvds.Where(t => t.Rating == rating);
            return listByRating.ToList();
        }

        public void DeleteDvd(int dvdId)
        {
            var deleteThis = _listOfDvds.SingleOrDefault(d => d.DvdId == dvdId);

            _listOfDvds.Remove(deleteThis);
        }

        public void UpdateDvd(Dvds dvd)
        {
            var oldDvd = _listOfDvds.SingleOrDefault(d => d.DvdId == dvd.DvdId);
            _listOfDvds.Remove(oldDvd);
            _listOfDvds.Add(dvd);
        }

        public void SaveDvd(Dvds dvd)
        {
            var maxDvdNumber = _listOfDvds.Max(m => m.DvdId);
            dvd.DvdId = maxDvdNumber + 1;
            _listOfDvds.Add(dvd);
        }
    }
}
