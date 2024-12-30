using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Reflection.Metadata;

namespace IEEE_EMB.Pages.Admin
{
    public class TaskAnswerModel : PageModel
    {
        public DB db { get; set; }
        [BindProperty(SupportsGet =true)]
        public int SessionId { get; set; }
        [BindProperty(SupportsGet = true)]

        public DataTable session { get; set; }
        public TaskAnswerModel(DB db)
        {
            this.db = db;
        }
        public void OnGet(int SessionId)
        {
            this.SessionId = SessionId;
            session=db.getspecificsession(SessionId);
        }
        public async Task<IActionResult> OnPost(IFormFile TASK_ANSWER) 
        {
            if (ModelState.IsValid) 
            {
                if (TASK_ANSWER != null && TASK_ANSWER.Length > 0)
                {
                    var filename1 = Guid.NewGuid() + Path.GetExtension(TASK_ANSWER.FileName);
                    var filepath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/TaskAnswer", filename1);
                    using (var stream = System.IO.File.Create(filepath1))
                    {
                        await TASK_ANSWER.CopyToAsync(stream);
                    }
                    db.AddSessionTaskAnswer(SessionId, filename1);
                    int activityId = db.ACTIVITYID(SessionId);
                    return RedirectToPage("/Admin/Session", new { activityId = activityId });

                }
            }
            return RedirectToPage("/Admin/TaskAnswer", new { SessionId = this.SessionId });
        }
    }
}
