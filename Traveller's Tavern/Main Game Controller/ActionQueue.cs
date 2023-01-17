using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maingamecontroller
{
    public class ActionQueue
    {
        public delegate void Action();

        private LinkedList<Action> Queue = new LinkedList<Action>();

        private Stack<Action> PastActions = new Stack<Action>();

        public void TakeNextAction()
        {
            Queue.First()();

            PastActions.Push(Queue.First());

            Queue.RemoveFirst();
        }

        public void QueueAction(Action action)
        {
            Queue.AddLast(action);
        }

        public void QueueActionNext(Action action)
        {
            if (Queue.First != null) Queue.AddAfter(Queue.First, action);

            else Queue.AddFirst(action);
        }

        public void UndoLastAction(Action action)
        {

        }
    }
}
