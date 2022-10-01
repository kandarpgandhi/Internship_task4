using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationAPI2.Models;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationAPI2.Controllers
{
    public class condoctorApiController : ApiController
    {
        ApiDatabaseEntities db = new ApiDatabaseEntities();
        
        [HttpGet]
        public IHttpActionResult Getdata()
        {
            Class1 obj=new Class1();
            DataSet ds = obj.selectDatabyId("spGetCondoctor");
            var l = ds.Tables[0].AsEnumerable().Select(DataRow => new condoctor { c_id = DataRow.Field<int>("c_id"), c_name = DataRow.Field<string>("c_name"), c_age = DataRow.Field<int>("c_age") }).ToList();
            return Ok(l);
        }

        [HttpGet]
        public IHttpActionResult Getdata(int id)
        {
            Class1 obj = new Class1();
            DataSet ds=obj.selectDatabyId($"spGetCndoctorById {id}");
            var l =ds.Tables[0].AsEnumerable().Select(DataRow=>new condoctor { c_id = DataRow.Field<int>("c_id"),c_name =DataRow.Field<string>("c_name"), c_age = DataRow.Field<int>("c_age") }).ToList();
            return Ok(l);
        }

       [HttpPost]
        public IHttpActionResult Insertdata(abcTest a)
        {
           
            int id = a.c_id;
            string name = a.name;
            int age=a.age;
            if (a.bus_id != null)
            {
                int bId = int.Parse(a.bus_id);
                common cm = new common();
                cm.c_id = id;
                cm.bus_id = bId;
                db.commons.Add(cm);
                db.SaveChanges();
            }
            condoctor c = new condoctor();
            c.c_id = id;
            c.c_name = name;
            c.c_age = age;
            db.condoctors.Add(c);
            db.SaveChanges();
            return Ok();
        }
        [Route("api/condoctorApi/InsertArrdata")]
        [HttpPost]
        public IHttpActionResult InsertArrdata(abcTest[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                int id = a[i].c_id;
                string name = a[i].name;
                int age = a[i].age;

                if (a[i].bus_id != null)
                {
                    int bId = int.Parse(a[i].bus_id);
                    common cm = new common();
                    cm.c_id = id;
                    cm.bus_id = bId;
                    db.commons.Add(cm);
                    db.SaveChanges();
                }
                condoctor c = new condoctor();
                c.c_id = id;
                c.c_name = name;
                c.c_age = age;
                db.condoctors.Add(c);
                db.SaveChanges();
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult updateData(abcTest obj)
        {
            var con=db.condoctors.Where(model=>model.c_id==obj.c_id).FirstOrDefault();
            if (con != null)
            {
                if (obj.bus_id!=null)
                {
                    int bId=int.Parse(obj.bus_id);
                    var com=db.commons.Where(model=>model.bus_id==bId).FirstOrDefault();
                    com.bus_id = bId;
                    db.SaveChanges();
                }
           
                con.c_id = obj.c_id;
                con.c_name = obj.name;
                con.c_age = obj.age;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        public class abcTest
        {
            //public int bus_id { get; set; }
            [NotMapped]
            public string bus_id { get; set; }
            public int bus_time { get; set; }
            public string bus_route { get; set; }
            public string name { get; set; }
            public int age { get; set; }
            public int c_id { get; set; }

        }
    }
}
