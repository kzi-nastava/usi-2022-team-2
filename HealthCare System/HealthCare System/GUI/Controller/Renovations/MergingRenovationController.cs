﻿using HealthCare_System.Core.Renovations;
using HealthCare_System.Core.Renovations.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.Renovations
{
    class MergingRenovationController
    {
        private readonly IMergingRenovationService mergingRenovationService;

        public MergingRenovationController(IMergingRenovationService mergingRenovationService)
        {
            this.mergingRenovationService = mergingRenovationService;
        }

        public List<MergingRenovation> MergingRenovations()
        {
            return mergingRenovationService.MergingRenovations();
        }

        public void BookRenovation(MergingRenovationDto mergingRenovationDto)
        {
            mergingRenovationService.BookRenovation(mergingRenovationDto);
        }

        public void StartMergingRenovation(MergingRenovation mergingRenovation)
        {
            mergingRenovationService.StartMergingRenovation(mergingRenovation);
        }

        public void FinishMergingRenovation(MergingRenovation mergingRenovation)
        {
            mergingRenovationService.FinishMergingRenovation(mergingRenovation);
        }

        public void TryToExecuteMergingRenovations()
        {
            mergingRenovationService.TryToExecuteMergingRenovations();
        }

        public MergingRenovation FindById(int id)
        {
            return mergingRenovationService.FindById(id);
        }

        public int GenerateId()
        {
            return mergingRenovationService.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/MergingRenovation_Room.csv")
        {
            mergingRenovationService.Serialize();
        }
    }
}
