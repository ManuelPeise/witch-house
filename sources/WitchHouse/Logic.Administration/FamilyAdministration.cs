﻿using Data.Shared.Entities;
using Data.Shared.Models.Export;
using Logic.Shared;
using Logic.Shared.Interfaces;
using System.Data;

namespace Logic.Administration
{
    public class FamilyAdministration : LogicBase
    {
        private readonly IAccountUnitOfWork _unitOfWork;
        
        public FamilyAdministration(IAccountUnitOfWork unitOfWork) : base()
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserDataExportModel>> GetFamilyUsers(Guid? familyId)
        {
            try
            {
                if (familyId == null || familyId == Guid.Empty)
                {
                    throw new Exception($"FamilyGuid could not be null!");
                }

                var entities = await _unitOfWork.AccountRepository.GetByAsync(acc => acc.FamilyGuid != null && acc.FamilyGuid == familyId);

                if (!entities.Any())
                {
                    throw new Exception($"Could not find users of [{familyId}]!");
                }


                var exportModels = (from e in entities
                                    select e.ToUserDataExportModel()).ToList();

                return exportModels;

            }
            catch (Exception exception)
            {
                await _unitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = familyId,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(FamilyAdministration),
                });

                await _unitOfWork.SaveChanges();

                return new List<UserDataExportModel>();
            }
        }

    }
}
