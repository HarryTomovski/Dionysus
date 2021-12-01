using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class EnvironmentalControllerDBAccess : IEnvironmentalControllerDBAccess
    {
        public async Task<int> setManualControl(bool enableManualControl, int pin, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var machine = await context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).FirstOrDefaultAsync();
                    machine.Mode = enableManualControl;
                    //update db
                    context.EnvironmentalControllers.Attach(machine);
                    context.Entry(machine).Property(m => m.Mode).IsModified = true;

                    //save changes
                    await context.SaveChangesAsync();
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<int> setMachineState(bool machineState, int pin, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var machine = await context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).FirstOrDefaultAsync();
                    machine.State = machineState;
                    //update db
                    context.EnvironmentalControllers.Attach(machine);
                    context.Entry(machine).Property(s => s.State).IsModified = true;

                    //save changes
                    await context.SaveChangesAsync();
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<bool> getMachineState(int pin, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var state = await context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).Select(s => s.State).FirstOrDefaultAsync();
                    return state;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public async Task<bool> getManualControl(int pin, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    bool state = await context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).Select(s => s.Mode).FirstOrDefaultAsync();
                    return state;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }



        public async Task<int> addEnvironmentalController(EnvironmentalController controller)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    await context.EnvironmentalControllers.AddAsync(controller);
                    await context.SaveChangesAsync();
                    return controller.ControllerPinNumber;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }
    }
}
