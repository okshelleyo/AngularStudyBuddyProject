using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyBuddyTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyBuddyTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyBuddyController : ControllerBase
    {
        private readonly StudyDBContext _context;
        public StudyBuddyController(StudyDBContext context)
        {
            _context = context;
        }


        #region Get/Read
        //GET: api/StudyBuddy
        [HttpGet("getallquestions")]
        public async Task<ActionResult<List<QandA>>> GetAllQandA()
        {
            var questions = await _context.QandAs.ToListAsync();
            return questions;
        }


        //GET: api/StudyBuddy/{Qid}
        [HttpGet("getquestion/{Qid}")]
        public async Task<ActionResult<QandA>> GetQandA(int Qid)
        {
            var question = await _context.QandAs.FindAsync(Qid);
            if (question == null)
            {
                return NotFound();
            }
            else
            {
                return question;
            }

        }

        //GET: api/StudyBuddy/favorite/{UserName}
        [HttpGet("getuser/{UserName}")]
        public async Task<ActionResult<List<QandA>>> GetFavorites(string UserName)
        {
            //attempting to join in C#
            //var x = await (from u in _context.Favorites join i in _context.QandAs on u.Qid equals i.Qid where u.UserName.Contains(UserName) select new {Qid = i.Qid, UserName = u.UserName, qs = i.Question } ).ToListAsync();
            var favorites = await _context.Favorites.Where(x => x.UserName == UserName).ToListAsync();
            var userQA = new List<QandA>();
            foreach (var a in favorites)
            {
                userQA.Add(await _context.QandAs.Where(x => x.Qid == a.Qid).FirstAsync());

            }

            if (userQA.Count > 0)
            {
                return userQA;
            }
            else
            {
                return NotFound();
            }
        }
        #endregion

        #region Create/Post/Add
        //POST api/StudyBuddy
        [HttpPost("addquestion")]
        public async Task<ActionResult<QandA>> AddQandA(QandA question)
        {
            _context.QandAs.Add(question);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetQandA), new { Qid = question.Qid }, question);
        }

        #endregion

        #region Delete
        //DELETE api/StudyBuddy/{Qid}
        [HttpDelete("deletequestion/{Qid}")]
        public async Task<ActionResult<QandA>> DeleteQandA(int Qid)
        {
            var question = await _context.QandAs.FindAsync(Qid);
            if (question == null)
            {
                return NotFound();
            }
            else
            {
                _context.QandAs.Remove(question);
                await _context.SaveChangesAsync();
                return NoContent();
            }

        }

        #endregion


    }
}
