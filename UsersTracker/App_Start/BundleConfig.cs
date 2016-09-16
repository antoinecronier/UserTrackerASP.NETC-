﻿using System.Web;
using System.Web.Optimization;

namespace UsersTracker
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/ProductCreate").Include(
                        "~/Scripts/ProductCreate.js"));

            bundles.Add(new ScriptBundle("~/bundles/AchatsCreate").Include(
                        "~/Scripts/AchatsCreate.js"));

            bundles.Add(new ScriptBundle("~/bundles/JQueryUI").Include(
                        "~/Scripts/jquery-ui-1.12.0.js"));

            bundles.Add(new StyleBundle("~/Content/JQueryUIDatepicker-css").Include(
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/themes/base/datepicker.css"));
        }
    }
}