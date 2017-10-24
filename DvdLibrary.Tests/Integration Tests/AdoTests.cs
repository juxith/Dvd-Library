using DvdLibrary.Data.Repos;
using DvdLibrary.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Tests.Integration_Tests.ADO
{
    [TestFixture]
    public class AdoTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void ADOCanLoadDvds()
        {
            var repo = new ADORepository();

            var dvds = repo.GetAll();

            Assert.AreEqual(3, dvds.Count);
            Assert.AreEqual("Judy Thao", dvds[0].Director);
            Assert.AreEqual("The Big Bad Wolf", dvds[0].Title);
        }

        [Test]
        public void ADOCanLoadByTitle()
        {
            var repo = new ADORepository();

            var dvds = repo.LoadListByTitle("The Big Bad Wolf");

            Assert.AreEqual(1, dvds.Count);
            Assert.AreEqual("Judy Thao", dvds[0].Director);
            Assert.AreEqual(1990, dvds[0].ReleaseYear);
            Assert.AreEqual("G", dvds[0].Rating);
        }

        [Test]
        public void ADOCanLoadByRating()
        {
            var repo = new ADORepository();

            var dvds = repo.LoadListByRating("PG");

            Assert.AreEqual(1, dvds.Count);
            Assert.AreEqual("Eric Wise", dvds[0].Director);
            Assert.AreEqual(1992, dvds[0].ReleaseYear);
            Assert.AreEqual("Little Red Riding Hood", dvds[0].Title);
        }

        [Test]
        public void ADOCanLoadByReleaseYear()
        {
            var repo = new ADORepository();

            var dvds = repo.LoadListByReleaseyear("1990");

            Assert.AreEqual(2, dvds.Count);
            Assert.AreEqual("Eric Ward", dvds[1].Director);
            Assert.AreEqual("Full Moon", dvds[1].Title);
            Assert.AreEqual("PG-13", dvds[1].Rating);
        }

        [Test]
        public void ADOCanLoadByDirector()
        {
            var repo = new ADORepository();

            var dvds = repo.LoadListByDirector("Eric Ward");

            Assert.AreEqual(1, dvds.Count);
            Assert.AreEqual(1990, dvds[0].ReleaseYear);
            Assert.AreEqual("Full Moon", dvds[0].Title);
            Assert.AreEqual("PG-13", dvds[0].Rating);
        }

        [Test]
        public void ADOCanLoadDvdDetail()
        {
            var repo = new ADORepository();

            var dvds = repo.LoadSingleDvdDetail(3);

            Assert.AreEqual(1990, dvds.ReleaseYear);
            Assert.AreEqual("Full Moon", dvds.Title);
            Assert.AreEqual("PG-13", dvds.Rating);
            Assert.AreEqual("Eric Ward", dvds.Director);
        }


        [Test]
        public void ADOCanCreateDvd()
        {
            var repo = new ADORepository();

            var dvds = new Dvds()
            {
                Title = "Trolls",
                ReleaseYear = 2017,
                Director = "Disney",
                Rating = "G",
                Notes = "Colorful and happy movie."
            };

            repo.SaveDvd(dvds);

            Assert.AreEqual(2017, dvds.ReleaseYear);
            Assert.AreEqual("Trolls", dvds.Title);
            Assert.AreEqual("G", dvds.Rating);
            Assert.AreEqual("Disney", dvds.Director);
            Assert.AreEqual(4, dvds.DvdId);
        }

        [Test]
        public void ADOCanUpdateDvd()
        {
            var repo = new ADORepository();

            var dvds = new Dvds()
            {
                Title = "Trolls",
                ReleaseYear = 2017,
                Director = "Disney",
                Rating = "G",
                Notes = "Colorful and happy movie."
            };

            repo.SaveDvd(dvds);

            dvds.Title = "Trololololololol";
            dvds.Rating = "R";

            repo.UpdateDvd(dvds);

            Assert.AreEqual(2017, dvds.ReleaseYear);
            Assert.AreEqual("Trololololololol", dvds.Title);
            Assert.AreEqual("R", dvds.Rating);
            Assert.AreEqual("Disney", dvds.Director);
            Assert.AreEqual(4, dvds.DvdId);
        }

        [Test]
        public void ADOCanDeleteDvd()
        {
            var repo = new ADORepository();

            repo.DeleteDvd(1);

            var dvds = repo.GetAll();

            Assert.AreEqual(2, dvds.Count);
            Assert.AreEqual(2, dvds[0].DvdId);
        }
    }
}
