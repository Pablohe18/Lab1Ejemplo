using EjemploLab1.DBContext;
using EjemploLab1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EjemploLab1.Controllers
{

    public class PlayerController : Controller
    {

        DefaultConnection db = DefaultConnection.getInstance;
        // GET: Jugador
        public ActionResult Index()
        {

            return View(db.Jugadores.ToList());
        }

        // GET: Jugador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Jugador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jugador/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Jugadorid,Nombre,Apellido,Salario,Posicion,Club")]AllPlayers jugador)
        {
            try
            {
                // TODO: Add insert logic here
                jugador.IDPlayer = ++db.IDActual;
                db.Jugadores.Add(jugador);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllPlayers jugadorBuscado = db.Jugadores.Find(x => x.IDPlayer == id);

            if (jugadorBuscado == null)
            {

                return HttpNotFound();
            }
            return View(jugadorBuscado);
        }

        // POST: Jugador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Jugadorid,Nombre,Apellido,Salario,Posicion,Club")]AllPlayers jugador)
        {
            try
            {
                // TODO: Add update logic here
                AllPlayers jugadorbuscado = db.Jugadores.Find(x => x.IDPlayer == jugador.IDPlayer);
                if (jugadorbuscado == null)
                {
                    return HttpNotFound();
                }
                jugadorbuscado.Name = jugador.Name;
                jugadorbuscado.LastName = jugador.LastName;
                jugadorbuscado.Salary = jugador.Salary;
                jugadorbuscado.Position = jugador.Position;
                jugadorbuscado.Equipo = jugador.Equipo;
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Jugador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllPlayers jugadorBuscado = db.Jugadores.Find(x => x.IDPlayer == id);

            if (jugadorBuscado == null)
            {

                return HttpNotFound();
            }
            return View(jugadorBuscado);
        }

        // POST: Jugador/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                db.Jugadores.Remove(db.Jugadores.First(x => x.IDPlayer == id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugador/CargarArchivo
        public ActionResult CargarArchivo()
        {
            return View();
        }


        // POST: Jugador/CargarArchivo
        [HttpPost]
        public ActionResult CargarArchivo(HttpPostedFileBase archivo)
        {
            string pathArchivo = string.Empty;
            if (archivo != null)
            {
                string path = Server.MapPath("~/Cargas/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                pathArchivo = path + Path.GetFileName(archivo.FileName);
                string extension = Path.GetExtension(archivo.FileName);
                archivo.SaveAs(pathArchivo);
                Random miRandom = new Random();
                string archivoCsv = directory.File.ReadAllText(pathArchivo);
                foreach (string lineas in archivoCsv.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(lineas))
                    {
                        var model = (new AllPlayers
                        {
                            IDPlayer = ++db.IDActual,
                            Equipo = Convert.ToString(lineas.Split(',')[0]),
                            LastName = Convert.ToString(lineas.Split(',')[1]),
                            Name = Convert.ToString(lineas.Split(',')[2]),
                            Position = Convert.ToString(lineas.Split(',')[3]),
                            Salary = Convert.ToDouble((lineas.Split(',')[4])),

                        });
                        db.Jugadores.Add(model);
                    }
                }
            }
            //return View(db.Jugadores);
            return RedirectToAction("Index");
        }
    }
}
