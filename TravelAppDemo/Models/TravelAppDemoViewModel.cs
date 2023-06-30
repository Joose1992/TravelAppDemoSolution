﻿
using TravelAppDataAccess.Context;
using TravelAppDataAccess.Model;
using TravelAppDataAccess.Repositories;

namespace TravelAppDemo.Models
{
    public class TravelAppDemoViewModel
    {
        private readonly TravelAppDemoRepositories _configuration;

        public List<TravelAppDemoModel> TravelAppDemosList { get; set; }
        public TravelAppDemoModel CurrentTravelAppDemo { get; set; }
        public bool IsActionSuccess { get; set; }
        public string ActionMessage { get; set; }
        public TravelAppDemoViewModel(TravelAppDemoContext configuration)
        {
            _configuration = new TravelAppDemoRepositories(configuration);
            TravelAppDemosList = GetAllTravelAppDemo();
            CurrentTravelAppDemo = TravelAppDemosList.FirstOrDefault();
        }

        public List<TravelAppDemoModel>? GetAllTravelAppDemo()
        {
            return _configuration.GetAllAppointments();
        }

        public TravelAppDemoViewModel(TravelAppDemoContext configuration, int TravelId)
        {
            _configuration = new TravelAppDemoRepositories(configuration);
            TravelAppDemosList = new List<TravelAppDemoModel>();
            if (TravelId > 0)
            {
                CurrentTravelAppDemo = GetTravelAppDemo(TravelId);

            }
            else
            {
                CurrentTravelAppDemo = new TravelAppDemoModel();
            }
             List<TravelAppDemoModel> GetAllTravelAppDemo()
            {
                return _configuration.GetAllAppointments();

            }
            TravelAppDemoModel GetTravelAppDemo(int TravelId)
            {
                return _configuration.GetAppointmentByID(TravelId);
            }
        }

        public void RemoveAppoinment(int id)
        {
            _configuration.Delete(id);
            TravelAppDemosList = GetAllTravelAppDemo();
            CurrentTravelAppDemo = TravelAppDemosList.FirstOrDefault();
        }

        public void SaveAppointment(TravelAppDemoModel models)
        {
            if (models.TravelId > 0)
            {
                _configuration.Update(models);
            }
            else
            {
                models.TravelId = _configuration.Create(models);
            }
            TravelAppDemosList = GetAllTravelAppDemo();
            CurrentTravelAppDemo = GetTravelAppDemo(models.TravelId);
        }


        public TravelAppDemoModel GetTravelAppDemo(int travelId)
        {
            return _configuration.GetAppointmentByID(travelId);   
        }
        List<TravelAppDemoModel> GetAllTravelAppDemoByCompleted(bool isCompleted)
        {
            return (List<TravelAppDemoModel>)_configuration.GetAll(isCompleted);

        }
    }
}
