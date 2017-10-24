using DvdLibrary.Data.Interface;
using DvdLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdLibrary.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        static IDvdRepository _dvdRepository = DvdFactory.Create();

        [Route("dvds/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            return Ok(_dvdRepository.GetAll());
        }

        [Route("api/zoom/")]
        public List<Dvds> GetDvds()
        {
            return _dvdRepository.GetAll();
        }

        [Route("dvd/{dvdId}")]
        [AcceptVerbs("Get")]
        public IHttpActionResult GetDvd(int dvdId)
        {
            var found = _dvdRepository.LoadSingleDvdDetail(dvdId);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [Route("dvd")]
        [AcceptVerbs("Post")]
        public IHttpActionResult AddDvd(Dvds dvd)
        {
            _dvdRepository.SaveDvd(dvd);

            return Created($"dvd/{dvd.DvdId}", dvd);
        }

        [Route("dvd/{dvdId}")]
        [AcceptVerbs("PUT")]
        public void EditDvd(int dvdId, Dvds dvd)
        {
            _dvdRepository.UpdateDvd(dvd);
        }

        [Route("dvd/{dvdId}")]
        [AcceptVerbs("Delete")]
        public void DeleteDvd(int dvdId)
        {
            _dvdRepository.DeleteDvd(dvdId);
        }

        [Route("dvds/title/{input}")]
        [AcceptVerbs("Get")]
        public IHttpActionResult LoadByTitle(string input)
        {
            return Ok(_dvdRepository.LoadListByTitle(input));

        }

        [Route("dvds/year/{input}")]
        [AcceptVerbs("Get")]
        public IHttpActionResult LoadByYear(string input)
        {
            return Ok(_dvdRepository.LoadListByReleaseyear(input));

        }

        [Route("dvds/director/{input}")]
        [AcceptVerbs("Get")]
        public IHttpActionResult LoadByDirector(string input)
        {
            return Ok(_dvdRepository.LoadListByDirector(input));

        }

        [Route("dvds/rating/{input}")]
        [AcceptVerbs("Get")]
        public IHttpActionResult LoadByRating(string input)
        {
            return Ok(_dvdRepository.LoadListByRating(input));
        }
    }
}
