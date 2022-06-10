using AntesQueVenca.Application.ViewModels;
using AntesQueVenca.Data.Repositories;
using AntesQueVenca.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AntesQueVenca.Web.Dashboard.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository = new CategoryRepository();

        [HttpGet]
        public IActionResult Register()
        {
            var categoryRepository = new CategoryRepository();
            var categories = categoryRepository.GetAll();
            List<CategoryViewModel>categoryListViewModel = new List<CategoryViewModel>();

            foreach (var category in categories)
            {
                categoryListViewModel.Add(new CategoryViewModel
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name
                });
            }

            return View(categoryListViewModel);
        }

        [HttpPost]
        public IActionResult RegisterCategory(CategoryViewModel categoryViewModel)
        {
            if (categoryViewModel!=null && !string.IsNullOrEmpty(categoryViewModel.Name))
            {
                Category category = new Category();
                category.Name = categoryViewModel.Name;
                category.SetFieldsInsert(2);
                _repository.Add(category);
                _repository.Commit();
            }

            return RedirectToAction("Register");
        }

        public IActionResult Delete(int id)
        {
            if (id >= 0)
            {
                var category = _repository.GetById(id);
                _repository.Remove(category);
                _repository.Commit();
            }

            return RedirectToAction("Register");
        }

        public IActionResult Update(CategoryViewModel viewModel)
        {
            if (viewModel != null)
            {
                var categoria = _repository.GetById(viewModel.CategoryId);
                categoria.Name = viewModel.Name;

                _repository.Update(categoria);
                _repository.Commit();
            }
            return RedirectToAction("Register");
        }
   }
}
