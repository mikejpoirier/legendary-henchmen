using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Henchman
{
    [Route("api/[controller]")]
    public class HenchmanController : Controller
    {
        private IHenchmanBLC _blc;

        public HenchmanController(IHenchmanBLC blc)
        {
            _blc = blc;
        }

        [HttpGet]
        public List<Henchman> Get()
        {
            try
            {
                return _blc.GetHenchmen();
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        [HttpGet("{name}")]
        public Henchman Get(string name)
        {
            try
            {
                if(String.IsNullOrWhiteSpace(name))
                    throw new HttpRequestException("Invalid name");

                return _blc.GetHenchman(name);
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        [HttpPost("SingleHenchman")]
        public Henchman Post([FromBody] Henchman henchman)
        {
            try
            {
                if(henchman == null)
                    throw new HttpRequestException("Invalid Henchman");

                return _blc.PostHenchman(henchman);
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        [HttpPost("MultipleHenchmen")]
        public List<Henchman> Post([FromBody] List<Henchman> henchmen)
        {
            try
            {
                if(henchmen == null || henchmen.Count == 0)
                    throw new HttpRequestException("Invalid Henchmans");

                return _blc.PostHenchmen(henchmen);
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }
    }
}
