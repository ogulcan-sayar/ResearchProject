using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MespEvents
{
    public interface IEvent { };

    public class MespEventManager
    {
        public List<IEvent> events;

        public MespEventManager()
        {
            events = new List<IEvent>();
        }

        public void SendEvent(IEvent e)
        {
            events.Add(e);
        }

        public bool ListenEvent<T>(out T eventData) where T : IEvent
        {
            foreach (var e in events)
            {
                if (e is T ee)
                {
                    eventData = ee;
                    events.Remove(e);
                    return true;
                }
            }

            eventData = default;
            return false;
        }


        public IEnumerable<T> ListenEvents<T>() where T : IEvent
        {
            IEvent[] tempEvents = events.ToArray();
            foreach (var e in tempEvents)
            {
                if (e is T ee)
                {
                    yield return (T)ee;
                    events.Remove(e);
                }
            }
        }
    }
}
