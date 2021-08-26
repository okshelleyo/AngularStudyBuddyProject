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
    public class FavoriteController : ControllerBase
    {
        private readonly StudyDBContext _context;
        public FavoriteController(StudyDBContext context)
        {
            _context = context;
        }

        //[HttpGet("{UserName}")]
        //public async Task<ActionResult<Favorite>> GetFavorites(string UserName)
        //{
        //    var favorites = await _context.Favorites.FindAsync(UserName);

        //    if (favorites.UserName == UserName)
        //    {
        //        return favorites;
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

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

        #region Create
        [HttpPost("addfavorite/{username}/{qid}")]
        public async Task<ActionResult<Favorite>> AddToFavorite(string userName, int qid)
        {
            Favorite fav = new Favorite();
            fav.Qid = qid;
            fav.UserName = userName;
            var favQs = await _context.Favorites.Where(x => x.UserName == userName).ToListAsync();
            Favorite question = new Favorite();
            foreach (var q in favQs)
            {
                if (q.Qid == qid)
                {
                    question = q;
                }
                else
                {
                    question = null;
                }
            }
            //var question = await _context.Favorites.FindAsync(favQ);
            if (question == null)
            {
                _context.Favorites.Add(fav);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetFavorites), new { Qid = fav.Qid }, fav);
            }
            else
            {
                return NoContent();
            }

        }
        #endregion

        #region Delete
        [HttpDelete("deletequestion/{favusername}/{favqid}")]
        public async Task<ActionResult<Favorite>> DeleteQandA(int favQid, string favUserName)
        {
            var favQs = await _context.Favorites.Where(x => x.UserName == favUserName).ToListAsync();
            Favorite question = null;
            foreach (var q in favQs)
            {
                question = (favQs.Where(x => x.Qid == favQid).First());
            }
            //var question = await _context.Favorites.FindAsync(favQ);
            if (question == null)
            {
                return NotFound();
            }
            else
            {
                _context.Favorites.Remove(question);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
        #endregion


    }
}
