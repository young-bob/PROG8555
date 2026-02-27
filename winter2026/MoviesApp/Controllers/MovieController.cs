// TODO: Bo Yang
// TODO: 9086117

using Microsoft.AspNetCore.Mvc;
using MoviesApp.Models;
using MoviesApp.Repositories;

namespace MoviesApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly IRepository _repository;

        public MovieController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var movies = _repository.GetAll();
            return View(movies);
        }

        public IActionResult GetById(int id)
        {
            var movie = _repository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public IActionResult EditItem(int id)
        {
            var movie = _repository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View("Edit",movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(movie);
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", movie);
        }

        public IActionResult DeleteItem(int id)
        {
            var movie = _repository.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View("Delete", movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
