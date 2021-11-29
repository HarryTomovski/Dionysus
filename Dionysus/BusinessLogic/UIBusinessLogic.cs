﻿using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.Models;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class UIBusinessLogic : IUIBusinessLogic
    {
        private IEnvironmentalReadingDBAccess environmentalReadingDBAccess;
        private IBatchDBAccess batchDBAccess;
        

        public UIBusinessLogic(IEnvironmentalReadingDBAccess environmentalReadingDBAccess, IBatchDBAccess batchDBAccess)
        {
            this.environmentalReadingDBAccess = environmentalReadingDBAccess;
            this.batchDBAccess = batchDBAccess;
        }
        public async Task<AvarageDataReadingDTO> getAvarageReadingForDate(DateTime date)
        {
            var readingsList = await environmentalReadingDBAccess.getReadingsForDate(date);
            if (readingsList is not null)
            {
                double? avarageTemperature = readingsList.Select(t => t.TemperatureReading).Average();
                double? avarageHumidity = readingsList.Select(h => h.HumidityReading).Average();

                var dto = new AvarageDataReadingDTO(avarageHumidity, avarageTemperature, date.Date);

                return dto;
            }
            else 
            {
                return null;
            }
        }

        public async Task<int> setHumidityTarget(double humidity, int batchId)
        {
            int result = await environmentalReadingDBAccess.setHumidityTarget(humidity, batchId);
            return result;
        }

        public async Task<int> setTemperatureTarget(double temperature, int batchId)
        {

            int result = await environmentalReadingDBAccess.setTemperatureTarget(temperature, batchId);
            return result;
        }

        public async Task<int> setManualControl(bool enableManualControl, int pin, int batchId)
        {
            int result = await environmentalReadingDBAccess.setManualControl(enableManualControl, pin, batchId);
            return result;
        }

        public async Task<int> setMachineState(bool machineState, int pin, int batchId)
        {
            int result = await environmentalReadingDBAccess.setMachineState(machineState, pin, batchId);
            return result;
        }

        public async Task<bool> getMachineState(int pin, int batchId)
        {
            var result = await environmentalReadingDBAccess.getMachineState(pin, batchId);
            return result;
        }

        public async Task<int> addBatch(Batch batch)
        {
            var result = await environmentalReadingDBAccess.addBatch(batch);
            return result;
        }

        public async Task<int> addEnvironmentalController(EnvironmentalController controller)
        {
            var result = await environmentalReadingDBAccess.addEnvironmentalController(controller);
            return result;
        }

        public async Task<int> addSensor(Sensor sensor)
        {
            var result = await environmentalReadingDBAccess.addSensor(sensor);
            return result;
        }

        public async Task<int> addRating(Rating rating)
        {
            var result = await environmentalReadingDBAccess.addRating(rating);
            return result;
        }
        ////To be removed since added to UserBusinessLogic
        //public async Task<User> addUser(User user, string? validationCode)
        //{
        //    var usernameValid = await environmentalReadingDBAccess.doesUsernameExsist(user.Username);
        //    if(usernameValid == false)
        //    {
        //        if(user.Role == UserEnums.Somelier.ToString())
        //        {
        //            var valCode = await environmentalReadingDBAccess.getValidationCode(validationCode);
        //            if(valCode is not null && valCode.Equals(validationCode))
        //            {
        //                var result = await environmentalReadingDBAccess.addUser(user);
        //                if(result is not null)
        //                {
        //                    await environmentalReadingDBAccess.removeValidationCode(validationCode);
        //                    return result;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var result = await environmentalReadingDBAccess.addUser(user);
        //            return result;
        //        }
        //    }
        //    return null;
        //}
        ////To be removed since addedd to UserBusinesslogic
        //public async Task<User> getUser(string username, string password)
        //{
        //    var user = await environmentalReadingDBAccess.getUser(username);
        //    if (user is not null && user.Password.Equals(password))
        //    {
        //        return user;
        //    }
        //    return null;
        //}

        public async Task<AvarageDataReadingDTO> getAvarageReadingSinceBeginning(DateTime date, int batchId)
        {
            var exists = await  batchDBAccess.batchExists(batchId);
            if (exists)
            {
                //if the batch exsists there is going to be a value
                var storedOn = await batchDBAccess.getStoredOn(batchId);
                if (storedOn.HasValue)
                {
                    //dont need to pass the stored on date because we can get in from the batch id
                    var storedOnValue = storedOn.Value;
                    var readingsList = await environmentalReadingDBAccess.getReadingsSinceBeginning(date, batchId, storedOnValue);

                    double? avarageTemperature = readingsList.Select(t => t.TemperatureReading).Average();
                    double? avarageHumidity = readingsList.Select(h => h.HumidityReading).Average();

                    var dto = new AvarageDataReadingDTO(avarageHumidity, avarageTemperature, date.Date);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
