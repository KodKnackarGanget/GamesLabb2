﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KKGGames_Labb2.Models;

namespace KKGGames_Labb2.Controllers
{
    public class TicTacToeController : Controller
    {
        // GET: TicTacToe
        public ActionResult Index()
        {
            var model = new TicTacToeModel();
            if(model.IsComputerTurn())
            {
                model.ComputerTurn();
                Session["CellBoard"] = model.CellBoard;
            }
            return View(model);
        }

        // POST: TicTacToe
        [HttpPost]
        public ActionResult Index(TicTacToeModel model, string chosenCell)
        {
            if (Session["CellBoard"] != null)
                model.CellBoard = (Cell[][])Session["CellBoard"];

            model.ParseChosenCell(chosenCell);
            if (model.TryPlaceChosenCell())
            {
                if (model.IsGameComplete)
                {
                    Session["CellBoard"] = null;
                    if(model.IsTie)
                        return View("Tie");
                    return View("Win");
                }

                model.ComputerTurn();
                if (model.IsGameComplete)
                {
                    Session["CellBoard"] = null;
                    if (model.IsTie)
                        return View("Tie");
                    return View("Lose");
                }
            }
            Session["CellBoard"] = model.CellBoard;
            return View(model);
        }
    }
}