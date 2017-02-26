using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class Manager
    {
        //Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {

        }

        /* START OF EMPLOYEE FUNCTIONS */

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBase>>(ds.Employees);
        }

        public EmployeeBase EmployeeGetOne(int id)
        {
            var o = ds.Employees.Find(id);
            return (o == null) ? null : Mapper.Map<Employee, EmployeeBase>(o);
        }

        internal object EmployeeEditContactInfo(int v)
        {
            throw new NotImplementedException();
        }

        public EmployeeBase EmployeeEditContactInfo(EmployeeEditContactInfo emp)
        {
            var o = ds.Employees.Find(emp.EmployeeId);

            if (o == null)
            {
                //Item was not found
                return null;
            }
            else
            {
                //Update object with incoming values
                ds.Entry(o).CurrentValues.SetValues(emp);

                //Save changes
                ds.SaveChanges();

                //Prepare and return the object
                return Mapper.Map<Employee, EmployeeBase>(o);

            }
        }
        /* !-END OF EMPLOYEE FUNCTIONS-! */


        /* START OF TRACK FUNCTIONS */

        public IEnumerable<TrackBase> TracksGetAll()
        {
            var query = ds.Tracks.OrderBy(x => x.TrackId);
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(query);
        }

        public IEnumerable<TrackBase> GetAllPopTracks()
        {
            //Pop track id = 9 Sorted by Track name
            var query = ds.Tracks
                            .Where(x => x.GenreId == 9)
                            .OrderBy(x => x.Name);

            //NOTE: Never return the result directly
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(query);
        }

        // Composer contains “Jon Lord”, sorted ascending by TrackId
        public IEnumerable<TrackBase> GetDeepPurpleTracks()
        {
            var query = ds.Tracks
                          .Where(x => x.Composer.Contains("Jon Lord"))
                          .OrderBy(x => x.TrackId);

            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(query);
        }

        //Sorted descending by Milliseconds. Limit the results to 100 items only.
        public IEnumerable<TrackBase> GetTop100LongestTracks()
        {
            var query = ds.Tracks.OrderByDescending(x => x.Milliseconds).Take(100);
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(query);
        }

        /* !-END OF TRACK FUNCTIONS-! */
    }
}