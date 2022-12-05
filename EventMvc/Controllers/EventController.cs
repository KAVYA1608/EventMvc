using EventMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace EventMvc.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEvents()
        {
            List<Event> EventList = new List<Event>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8032/api/Event/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET - Get all the students
                var responseTask = client.GetAsync("getallevents");
                // HTTP Get with id and name parameters
                //var responseTask = client.GetAsync(String.Format("values/getstudentname/?id={0}&name={1}", 25, "Stephen"));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back student object list
                    var readTask = response.Content.ReadAsAsync<List<Event>>();
                    readTask.Wait();
                    EventList = readTask.Result;
                }
            }
            return View(EventList);
        }

        //[HttpGet]
        //public ActionResult GetEventById(int evntid)
        //{
        //    Event evt = new Event();
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:8088/api/event/");
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        //HTTP GET - Get a student by the Student Id
        //        var responseTask = client.GetAsync("EventById/?evId = {0}, evntid");
        //        // HTTP Get with id and name parameters
        //        //var responseTask = client.GetAsync(String.Format("getstudentByname/?id={0}&name={1}", StdId, StdName));
        //        responseTask.Wait();

        //        HttpResponseMessage response = responseTask.Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Get back a single student object
        //            var readTask = response.Content.ReadAsAsync<Event>();
        //            readTask.Wait();
        //            evt = readTask.Result;
        //        }
        //    }
        //    return View(evt);
        //}



        public ActionResult AddEvent()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddEvent(Event ed)
        {
            String res = "";
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8032/api/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync(String.Format("event/AddEvent"), ed);
                    responseTask.Wait();

                    HttpResponseMessage response = responseTask.Result;
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    var readTask = response.Content.ReadAsStringAsync();
                    //    readTask.Wait();
                    //    res = readTask.Result;

                    //}

                }
                //// If successfully Inserted the Student record
                //if (Convert.ToInt32(res) > 0)
                //    return RedirectToAction("GetAllEvents");
                //else
                //    // Something went wrong in the adding the record.
                //    // Check the exception Log.
                //    // Post back the student to the Edit View itself.
                //    return View(ed);
            }
            return RedirectToAction("GetEvents");
        }


        [HttpGet]
        //Get of Edit Event
        public ActionResult EditEvent(int id)
        {
            Event e1 = new Event();
            // Get the Student object and pass it to the view, for Confirmation
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8032/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET - Get a Event by the Student Id
                var responseTask = client.GetAsync("event/EditEvent/?evId=" + id);
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back a single student object
                    var readTask = response.Content.ReadAsAsync<Event>();
                    readTask.Wait();
                    e1 = readTask.Result;
                }

            }
            return View(e1);
        }


        [HttpPost]
        // Post back from the Edit Event View
        public ActionResult EditEvent(Event e)
        {
            String res = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8032/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP PUT, pass the Student Id and the Student Object
                var responseTask = client.PutAsJsonAsync(String.Format("event/EditEvent"), e);
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                //        if (response.IsSuccessStatusCode)
                //        {
                //            var readTask = response.Content.ReadAsStringAsync();
                //            readTask.Wait();
                //            res = readTask.Result;
                //        }

                //    }
                //    // If successfully Updated the Student record
                //    if (Convert.ToInt32(res) > 0)
                //        return RedirectToAction("GetAllEvents");
                //    else
                //        // Something went wrong in the adding the record.
                //        // Check the exception Log.
                //        // Post back the student to the Edit View itself.
                //        return View(e);
                //}
                //else
            }
            return RedirectToAction("GetEvents");
        }


        [HttpGet]
        public ActionResult DeleteEvent(int id)
        {
            Event e1 = new Event();
            // Get the Student object and pass it to the view, for Confirmation
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8032/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET - Get a student by the Student Id
                var responseTask = client.GetAsync("event/DeleteEventss/?eid=" + id);
                // HTTP Get with id and name parameters
                //var responseTask = client.GetAsync(String.Format("getstudentByname/?id={0}&name={1}", StdId, StdName));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back a single student object
                    var readTask = response.Content.ReadAsAsync<Event>();
                    readTask.Wait();
                    e1 = readTask.Result;
                }
                return View(e1);
            }
        }



        // Post back from the Delete
        [HttpPost, ActionName("DeleteEvent")]
        public ActionResult DeleteEvents(int id)
        {
            List<Event> TD = new List<Event>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8032/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var responseTask = client.DeleteAsync(String.Format("event/DeleteEvents/?id=" + id));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    // Get back student object
                //    var readTask = response.Content.ReadAsAsync<List<Event>>();
                //    readTask.Wait();
                //    //TD = readTask.Result;


                //}
            }
            return RedirectToAction("GetEvents");
        }
    }
}