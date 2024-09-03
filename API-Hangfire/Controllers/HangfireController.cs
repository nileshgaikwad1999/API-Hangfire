using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace API_Hangfire.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HangfireController : ControllerBase
    {
        //fire and foregt job
        [HttpPost]
        public IActionResult Welcome()
        {
            var jobId = BackgroundJob.Enqueue(() => sendWelcomeEmail("welcome to our app"));
            return Ok($"{jobId}welcome email send to the user");
        }
        public void sendWelcomeEmail(string test)
        {
            Console.WriteLine("welcome email sent to the user");
        }
        //dealyed job
        [HttpPost]
        public IActionResult Discount()
        {
            var jobid = BackgroundJob.Schedule(() => sendWelcomeEmail("welcome to our app"), TimeSpan.FromSeconds(40));
            return Ok($"{jobid} discount email send to email in 30 second");
        }

        //recurring job
        [HttpPost]
        public IActionResult DataBaseUpdate()
        {
            RecurringJob.AddOrUpdate(() => Console.WriteLine("databseupdated"), Cron.Minutely);
            return Ok("Test data  updated ");
        }
        [HttpPost]
        public IActionResult confirm()
        {
            var jobid = BackgroundJob.Schedule(() => Console.WriteLine("you asked to be unsubscribed!"), TimeSpan.FromSeconds(30));
            BackgroundJob.ContinueJobWith(jobid ,() => Console.WriteLine("unsubscribed!"));
            return Ok($"{jobid} confirm job creaded");
        }
    }
}