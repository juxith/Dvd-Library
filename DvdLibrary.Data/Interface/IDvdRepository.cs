using DvdLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Data.Interface
{
    public interface IDvdRepository
    {
        List<Dvds> GetAll();
        Dvds LoadSingleDvdDetail(int dvdId);
        List<Dvds> LoadListByTitle(string title);
        List<Dvds> LoadListByReleaseyear(string releaseYear);
        List<Dvds> LoadListByDirector(string director);
        List<Dvds> LoadListByRating(string rating);
        void DeleteDvd(int dvdId);
        void UpdateDvd(Dvds dvd);
        void SaveDvd(Dvds dvd);
    }
}
