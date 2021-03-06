using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeityOnceLost.UserInterface
{
    /// <summary>
    /// Used for storing user interface elements (called Clickables). Each UserInterface keeps
    /// their Clickables in a sorted list - the closer a Clickable is to the front of the list,
    /// the closer it is to the front of the screen. If multiple Clickables overlap, them being
    /// kept in a sorted list will tell the engine which is most relevant if the user clicks on
    /// that overlap.
    /// </summary>
    public class UserInterface
    {
        private List<Clickable> _clickables;

        public UserInterface()
        {
            _clickables = new List<Clickable>();
        }

        //Getter
        public List<Clickable> getClickables()
        {
            return _clickables;
        }

        //Setters, kinda
        public void addClickableToFront(Clickable clickable)
        {
            _clickables.Insert(0, clickable);
        }
        public void addClickableToBack(Clickable clickable)
        {
            _clickables.Add(clickable);
        }

        /// <summary>
        /// Add a new Clickable to the list at the specified index.
        /// </summary>
        public void addSortedClickable(Clickable clickable, int index)
        {
            _clickables.Insert(index, clickable);
        }

        /// <summary>
        /// Add a new Clickable to the list directly behind the specified Clickable: behind.
        /// If behind is not found, will add at the end of the list.
        /// </summary>
        public void addSortedClickable(Clickable clickable, Clickable behind)
        {
            if (_clickables.Contains(behind))
            {
                int index = _clickables.IndexOf(behind);
                _clickables.Insert(index, clickable);
            }
            else
            {
                _clickables.Add(clickable);
            }
        }

        /// <summary>
        /// Empties the _clickables list. Useful when rebuilding a UserInterface
        /// </summary>
        public void resetClickables()
        {
            _clickables.Clear();
        }

        /// <summary>
        /// Returns the most relevant Clickable at the given mouse coordinates (the
        /// front of the list). If none are found, returns null
        /// </summary>
        public Clickable getFrontClickable(Point mousePos)
        {
            for (int i = 0; i < _clickables.Count; i++)
            {
                if (_clickables[i].mouseInBoundaries(mousePos))
                {
                    return _clickables[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the most relevant Clickable at the given mouse coordinates (the
        /// front of the list) from an entire list of UserInterfaces. Iterates through
        /// each UserInterface's list of Clickables, from index 0 to max, until a
        /// return value that isn't null is found, and returns that. null will still
        /// be returned, however, if none are found throughout the whole list.
        /// 
        /// Updated to first check the hovered Clickable passed to it, if it's not null.
        /// </summary>
        public static Clickable getFrontClickableFromUIList(Clickable hoveredClickable, List<UserInterface> uiList, Point mousePos)
        {
            if (hoveredClickable != null && hoveredClickable.mouseInBoundaries(mousePos))
            {
                return hoveredClickable;
            }

            if (uiList != null)
            {
                for (int uiI = 0; uiI < uiList.Count; uiI++)
                {
                    Clickable returnClickable = uiList[uiI].getFrontClickable(mousePos);
                    if (returnClickable != null)
                    {
                        return returnClickable;
                    }
                }
            }

            return null;
        }
    }
}
