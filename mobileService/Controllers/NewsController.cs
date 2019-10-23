using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mService.Models;
using System.Data.SqlClient;
using System.Globalization;
using Repositories;

namespace mService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
 //   [RESTAuthorize]
    public class NewsController : ApiController
    {

        OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/News/GetNews

        [HttpGet]
        public List<News> GetNews()
        {
            var dataTable = bOra.GetNews();
            List<News> News = new List<News>();
            foreach (DataRow record in dataTable.Rows)
            {
                News.Add(new News()
                {
                    TITLE = record["TITLE"].ToString(),
                    NEWS_DATE = record["NEWS_DATE"].ToString(),
                    IMAGE_URL = record["IMAGE_URL"].ToString(),
                    DESCRIPTION = record["DESCRIPTION"].ToString(),


                });
            }

            return News;
        }
    }
}