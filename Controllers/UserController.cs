using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index(string searchString)
    {
        var users = userlist.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            users = users.Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString));
        }

        return View(users.ToList());
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Find the user by id
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            // Return NotFound if user does not exist
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Return the Create view
        return View();
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        // Add the new user to the list if model is valid
        if (ModelState.IsValid)
        {
            // Assign a new Id (auto-increment)
            user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1;
            userlist.Add(user);
            return RedirectToAction(nameof(Index));
        }
        // If model is invalid, return to Create view with validation errors
        return View(user);
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        // Find the user to edit
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        // Find the user to update
        var existingUser = userlist.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            // Update user properties
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            // Add other properties as needed
            return RedirectToAction(nameof(Index));
        }
        // If model is invalid, return to Edit view with validation errors
        return View(user);
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        // Find the user to delete
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        // Remove the user from the list
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        userlist.Remove(user);
        return RedirectToAction(nameof(Index));
    }
}
