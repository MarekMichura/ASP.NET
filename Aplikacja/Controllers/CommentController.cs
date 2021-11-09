﻿using Aplikacja.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Aplikacja.Controllers
{
    public class CommentController : Controller
    {
        static private List<Comment> comments = new List<Comment>() {
            new Comment()
            {
                CommentText = "Ha ha ha",
                UserID = 0,
                ImageID = 0
            },
            new Comment()
            {
                CommentText = "Lol fajne",
                UserID = 0,
                ImageID = 0
            },
            new Comment()
            {
                CommentText = "Kopiuje",
                UserID = 0,
                ImageID = 0
            }
        };

        static private int Count { get => comments.Count + 1; }

        public IActionResult Index()
        {
            //return View("tak");
            return View(model: comments);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult AddComment(Comment comment)
        {

            if (ModelState.IsValid)
            {
                comments.Add(comment);
                return View("Index", comments);
            }
            else
            {
                return View("Add");
            }
        }
    }
}
