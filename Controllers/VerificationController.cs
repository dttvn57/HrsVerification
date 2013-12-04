using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using HrsVerification.Models;

namespace HrsWorkedVerification.Controllers
{

    //public class Branch
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    //public class BranchViewModel
    //{
    //    public List<Branch> _branches;


    //    [Display(Name = "Branches")]
    //    public int SelectedBranchId { get; set; }

    //    public IEnumerable<SelectListItem> BranchItems
    //    {
    //        get { return new SelectList(_branches, "Id", "Name"); }
    //    }
    //}

    public abstract class ApplicationController : Controller
    {
        TimeVerificationDb _timeVerificationDB = new TimeVerificationDb();

        public static TimeVerificationForm _theForm = null;   //new TimeVerificationForm();

        public TimeVerificationDb DataContext
        {
            get { return _timeVerificationDB; }
        }

        public TimeVerificationForm theForm
        {
            get { return _theForm; }
            set { _theForm = value; }

        }

        public ApplicationController()
        {
           // ViewData["categories"] = from c in DataContext.MovieCategories
           //                          select c;
        }

    }


   //------------------------------------------------------------------------
    [Authorize]
    public class VerificationController : ApplicationController
    {
//trung        
        //TimeVerificationDb  _timeVerificationDB = new TimeVerificationDb();

        //List<WorkedTime> _workedTimeList = null;

        //
        // GET: /Verification/

         public ActionResult Index()
        {
            return View();
        }


         public ActionResult Create(string EmploymentStatus, string EmployeeID, bool reset = true)
         {
            //ViewBag.EmpStatus = EmploymentStatus;
            ViewBag.IsSAN = EmploymentStatus == "SAN" ? true : false;

           // EnumEmpStatus id = model.EmpStatus;
            //return Json('true, JsonRequestBehavior.AllowGet);


            //List<SelectListItem> branches = new List<SelectListItem>();
            //trung
            //foreach (var b in DataContext.BranchList)
            //{
            //    branches.Add(new SelectListItem { Text = b.FullName() });
            //}
            //branches.Add(new SelectListItem { Text = "ALB (360210)", Value = "0" });
            //branches.Add(new SelectListItem { Text = "CSV (360220)", Value = "1" });
            //branches.Add(new SelectListItem { Text = "DUB (360240)", Value = "2" });
            //branches.Add(new SelectListItem { Text = "NWK (360280)", Value = "3" });
            //branches.Add(new SelectListItem { Text = "SLZ (360300)", Value = "4" });
            //branches.Add(new SelectListItem { Text = "UCY (360310)", Value = "5" });
            //branches.Add(new SelectListItem { Text = "FRM (360260)", Value = "6" });
            //branches.Add(new SelectListItem { Text = "FRM J (360262)", Value = "7" });
            //branches.Add(new SelectListItem { Text = "CTV (360230)", Value = "8" });
            //branches.Add(new SelectListItem { Text = "IRV (360270)", Value = "9" });
            //branches.Add(new SelectListItem { Text = "NLS (360290)", Value = "10" });
            //branches.Add(new SelectListItem { Text = "EXT-CORE (360250)", Value = "11" });
            //branches.Add(new SelectListItem { Text = "BKM (360251)", Value = "12" });
            //branches.Add(new SelectListItem { Text = "JAILS (360252)", Value = "13" });
            //branches.Add(new SelectListItem { Text = "SR OUTREACH (360253)", Value = "14" });
            //branches.Add(new SelectListItem { Text = "COMM REL (360115)", Value = "15" });
            //branches.Add(new SelectListItem { Text = "ADULT PROG (360115)", Value = "16" });
            //branches.Add(new SelectListItem { Text = "CHILD SERV (360115)", Value = "17" });
            //branches.Add(new SelectListItem { Text = "COLL DEV (360116)", Value = "18" });
            //branches.Add(new SelectListItem { Text = "OTHER", Value = "19" });
            //ViewBag.Branches = branches;

            AddBranches();

            //TimeVerificationForm form = new TimeVerificationForm();
            if (reset)
            {
                if (theForm != null)
                {
                    theForm = null;
                }
                theForm = new TimeVerificationForm();

                // create the HOME branches
                AddHomeBranches();

                if (EmploymentStatus == "SAN")
                    theForm.EmpStatus = EnumEmpStatus.SAN;
                else
                    if (EmploymentStatus == "Permanent")
                        theForm.EmpStatus = EnumEmpStatus.Permanent;
                    else
                        theForm.EmpStatus = EnumEmpStatus.Temporary;

                theForm.EmpIdNum = EmployeeID;
                TempData["TimeVerificationForm"] = theForm;
            }
            else
            {
                AddHomeBranches(theForm.HomeBranchOrgId);
            }
            return View("Form", theForm);
         }

        [HttpPost]
         public ActionResult Create(TimeVerificationForm form, string HomeBranches, string sDateIn)
         {
            try
            {
                // synch with the newest info
                 if (theForm != null)
                 {
                     theForm.EmpFullName = form.EmpFullName;
                     theForm.Title = form.Title;
                     theForm.HomeBranchOrgId = HomeBranches;
                     DataContext.TimeVerificationFormList.Add(theForm);
                     DataContext.SaveChanges();
                 }
            }
            catch
            {
            }

            TempData["STATUS"] = "Form has been sent to the manager.";
            return RedirectToAction("Confirm", "Home"); 
         }

        public ActionResult AddRow(string paramsVal)
        // paramsVal has the format:  "Employ Name" + '!' + "Title" + '!' + "undefined" (or Homebranch index)
                            // homeBranch is a string that designates the index of the dropbox item, e.g. "1" is the 2nd item
         {
             AddBranches();
             ViewBag.IsSAN = theForm.EmpStatus == EnumEmpStatus.SAN ? true : false;

             string fullName = "";
             string title = "";
             string homeBranch = "";

             if (paramsVal != null)
             {
                 char[] sep = { '!' };
                 string[] str = paramsVal.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                 string[] s1 = str[0].Split('=');
                 fullName = s1[1];

                 string[] s2 = str[1].Split('=');
                 title = s2[1];

                 string[] s3 = str[2].Split('=');
                 homeBranch = s3[1].Equals("undefined", StringComparison.OrdinalIgnoreCase) ? null : str[2];
             }
             theForm.EmpFullName = fullName;
             theForm.Title = title;
             AddHomeBranches(homeBranch);

             if (homeBranch != null && homeBranch != "")
             {
                 theForm.HomeBranchOrgId = homeBranch;
             }

             if (theForm.WorkedTimes == null)
                 theForm.WorkedTimes = new List<WorkedTime>();
             return View("_WorkScheduleDetailView");
             //return PartialView("_WorkScheduleDetailView", );
         }

         [HttpPost]
         public ActionResult AddRow(WorkedTime workTime, string Branches, string IsSAN, TimeVerificationForm theForm1, string EmpFullName)
         {
             if (theForm.WorkedTimes == null)
                 theForm.WorkedTimes = new List<WorkedTime>();
             workTime.WorkBranchOrgId = Branches;
             theForm.WorkedTimes.Add(workTime);

             //AddBranches();
            // AddHomeBranches(theForm.HomeBranchOrgId);
             //ViewBag.IsSAN = IsSAN.Equals("true", StringComparison.OrdinalIgnoreCase) ? true : false;
             //TempData["TimeVerificationForm"] = theForm;

             return RedirectToAction("Create", new { EmploymentStatus = "", EmployeeID = "", reset = false });
             //return View("Form", theForm);
         }

         private void AddBranches()
         {
             List<SelectListItem> branches = new List<SelectListItem>();
             foreach (var b in DataContext.BranchList)
             {
                 branches.Add(new SelectListItem { Text = b.FullName() });
             }
             ViewBag.Branches = branches;
         }

         private void AddHomeBranches(string selectedBranchIndex = "")
         {
             List<SelectListItem> homeBranches = new List<SelectListItem>();
             homeBranches.Add(new SelectListItem 
                                    { 
                                        Text = "ALB",
                                        Selected = selectedBranchIndex == "ALB" ? true : false
                                    });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "CSV",
                                         Selected = selectedBranchIndex == "CSV" ? true : false
                                     });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "CTV",
                                         Selected = selectedBranchIndex == "CTV" ? true : false
                                     });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "DUB",
                                         Selected = selectedBranchIndex == "DUB" ? true : false
                                     });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "EXT",
                                         Selected = selectedBranchIndex == "EXT" ? true : false
                                     });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "FRM",
                                         Selected = selectedBranchIndex == "FRM" ? true : false
                                     });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "IRV",
                                         Selected = selectedBranchIndex == "IRV" ? true : false
                                     });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "NWK",
                                         Selected = selectedBranchIndex == "NWK" ? true : false
                                     });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "NLS",
                                         Selected = selectedBranchIndex == "NLS" ? true : false
                                     });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "SLZ",
                                         Selected = selectedBranchIndex == "SLZ" ? true : false
                                     });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "UCY",
                                         Selected = selectedBranchIndex == "UCY" ? true : false
                                     });
             homeBranches.Add(new SelectListItem
                                     {
                                         Text = "OTHER",
                                         Selected = selectedBranchIndex == "OTHER" ? true : false
                                     });
             ViewBag.HomeBranches = homeBranches;

             //List<SelectListItem> homeBranches = new List<SelectListItem>();
             //homeBranches.Add(new SelectListItem { Text = "ALB", Value = "0" });
             //homeBranches.Add(new SelectListItem { Text = "CSV", Value = "1" });
             //homeBranches.Add(new SelectListItem { Text = "CTV", Value = "2" });
             //homeBranches.Add(new SelectListItem { Text = "DUB", Value = "3" });
             //homeBranches.Add(new SelectListItem { Text = "EXT", Value = "4" });
             //homeBranches.Add(new SelectListItem { Text = "FRM", Value = "5" });
             //homeBranches.Add(new SelectListItem { Text = "IRV", Value = "6" });
             //homeBranches.Add(new SelectListItem { Text = "NWK", Value = "7" });
             //homeBranches.Add(new SelectListItem { Text = "NLS", Value = "8" });
             //homeBranches.Add(new SelectListItem { Text = "SLZ", Value = "9" });
             //homeBranches.Add(new SelectListItem { Text = "UCY", Value = "10" });
             //homeBranches.Add(new SelectListItem { Text = "OTHER", Value = "11" });
             //ViewBag.HomeBranches = homeBranches;
         }

         private string getHomeBranch(string homeBranchIndex)
         {
             if (homeBranchIndex == "0") return "ALB";
             if (homeBranchIndex == "1") return "CSV";
             if (homeBranchIndex == "2") return "CTV";
             if (homeBranchIndex == "3") return "DUB";
             if (homeBranchIndex == "4") return "EXT";
             if (homeBranchIndex == "5") return "FRM";
             if (homeBranchIndex == "6") return "IRV";
             if (homeBranchIndex == "7") return "NWK";
             if (homeBranchIndex == "8") return "NLS";
             if (homeBranchIndex == "9") return "SLZ";
             if (homeBranchIndex == "10") return "UCY";
             if (homeBranchIndex == "11") return "OTHER";

             return "ALB";
         }
    }
}
