using GPU.Helpers;
using GPU.Models;
using GPU.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GPU.Controllers
{
    [Authorize]
    public class StudentStages : Controller
    {
        ObservableCollection<GPU.Models.StudentStages> list = StudentServices._Stages;
        ObservableCollection<GPU.Models.StudentStages> Arlist = ArchiveService.ar_Stages;
        [Authorize(Policy = "RequireStuList")]
        public IActionResult Index(int? id)
        {
            var listModel = list.Where(x => x.Sid == id);
            var mo = new GPU.Models.StudentStages()
            {
                Name = StudentServices._Student.FirstOrDefault(x => x.Id == id).Name,
                Sid = (int)id
            };
            var ViewModel = (List: listModel.ToList(), stages: mo);
            return View(ViewModel);
        }
        [HttpPost]
        [Authorize(Policy = "RequireStuList")]
        public IActionResult Search([Bind(Prefix = "stages")] GPU.Models.StudentStages searchModel, int? id)
        {
            Debug.WriteLine(id);
            if (searchModel.Year != "-")
            {
                var StudentStages = list.Where(x => x.Year == searchModel.Year && x.Sid == id);
                searchModel.Sid = (Int32)id;
                searchModel.Name = StudentServices._Student.FirstOrDefault(x => x.Id == id).Name;
                var viewModel = (StudentStages.ToList(), searchModel);
                return View("Index", viewModel);
            }
            else
            {
                return RedirectToAction("Index", new { id = id });
            }
        }

        [HttpPost]
        [Authorize(Policy = "RequireStuList")]
        public async Task<IActionResult> NewStage([Bind(Prefix = "stages")] GPU.Models.StudentStages stages)
        {
            await StudentStagesHelper.AddNewStage(stages);
            return RedirectToAction("Index", new { id = stages.Sid });
        }
        
        [HttpPost]
        [Authorize(Policy = "RequireStuList")]
        public async Task<IActionResult> UpdateStage([Bind(Prefix = "stages")] GPU.Models.StudentStages stages,int? sid)
        {
            await StudentStagesHelper.EditStage(stages);
            return RedirectToAction("Index", new { id = stages.Sid });
        }

        [Authorize(Policy = "RequireArchList")]
        public IActionResult ArIndex(int? id)
        {
            var listModel = Arlist.Where(x => x.Sid == id);
            var mo = new GPU.Models.StudentStages()
            {
                Name = ArchiveService.ar_Student.FirstOrDefault(x => x.Id == id).Name,
                Sid = (int)id
            };
            var ViewModel = (List: listModel.ToList(), stages: mo);
            return View(ViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "RequireArchList")]
        public async Task<IActionResult> ArNewStage([Bind(Prefix = "stages")] GPU.Models.StudentStages stages)
        {
            await StudentStagesHelper.AddNewStage(stages,1);
            return RedirectToAction("ArIndex", new { id = stages.Sid });
        }
        [HttpPost]
        [Authorize(Policy = "RequireArchList")]
        public async Task<IActionResult> ArUpdateStage([Bind(Prefix = "stages")] GPU.Models.StudentStages stages, int? sid)
        {
            await StudentStagesHelper.EditStage(stages,1);
            return RedirectToAction("ArIndex", new { id = stages.Sid });
        }
        [HttpPost]
        [Authorize(Policy = "RequireArchList")]
        public IActionResult ArSearch([Bind(Prefix = "stages")] GPU.Models.StudentStages searchModel, int? id)
        {
            Debug.WriteLine(id);
            if (searchModel.Year != "-")
            {
                var StudentStages = Arlist.Where(x => x.Year == searchModel.Year && x.Sid == id);
                searchModel.Sid = (Int32)id;
                searchModel.Name = ArchiveService.ar_Student.FirstOrDefault(x => x.Id == id).Name;
                var viewModel = (StudentStages.ToList(), searchModel);
                return View("ArIndex", viewModel);
            }
            else
            {
                return RedirectToAction("ArIndex", new { id = id });
            }
        }

    }
}
