using ESAPIX.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlanChecker.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private IScriptContext _ctx;

        public MainViewModel(IScriptContext ctx)
        {
            _ctx = ctx;

            //Example data bind
            Result = "PASSED";

            ButtonCommand = new DelegateCommand(() =>
            {
                // MessageBox.Show("PRESSED!");
                //EValuate the plan
                //convert thhe plan ID to the actual plan.
                var selected = _ctx.PlansInScope.First(p => p.Id == SelectedPlan);
                Result = SelectedPlan + "PASSED";
            });
            ctx.CourseChanged += Ctx_CourseChanged;

            
        }
        
        private void Ctx_CourseChanged(ESAPIX.Facade.API.Course c)
        {
            FillPlans();
        }

        private void FillPlans()
        {
            //Fills the list with plan Ids
            if(_ctx.PlansInScope == null) { return; }
            Plans.Clear(); 
            foreach(var plan in _ctx.PlansInScope)
            {
                Plans.Add(plan.Id);
            }
        }

        private string result;

        public string Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }

        public DelegateCommand ButtonCommand { get; set; }

        //List for user interfaces
        public ObservableCollection<string> Plans { get; set; } = new ObservableCollection<string>();

        private string selectedplan;

        public string SelectedPlan
        {
            get { return selectedplan; }
            set { SetProperty(ref selectedplan, value); }
        }

       
    }


}
