using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ProjektDB
{
    public static class TreeViewExtension
    {
        static bool[] tviState;

        public static int ExpandedItemsCount(this TreeView treeView)
        {
            int i = 0;
            foreach (TreeViewItem it in treeView.Items)
            {
                i = it.IsExpanded ? ++i : i;
            }

            return i;
        }

        public static int MatchesTag(this TreeView treeView, string Match)
        {
            int i = 0;
            foreach (TreeViewItem it in treeView.Items)
            {
                i = it.Tag.Equals(Match) ? ++i : i;
            }

            return i;

        }

        public static void SetTreeViewExpandedState(this TreeView treeView,string TagMatch)
        {
            if (tviState != null)
            {
                int i = 0;
                foreach (TreeViewItem item in treeView.Items)
                {
                   
                    if (item.Tag.Equals(TagMatch))
                    {
                        item.IsExpanded = tviState[i++];

                    }
                }
            }
        }

        /// <summary>
        /// TreeView Status auslesen - Welche Gruppen sind ausgeklappt
        /// </summary>
        public static void GetTreeViewExpandedState(this TreeView treeView, string TagMatch)
        {
            if (tviState == null)
            {
                int count = treeView.MatchesTag(TagMatch);
                //int c = MainTreeView.Items.Count;
                tviState = new bool[count];
            }

            int i = 0;
            foreach (TreeViewItem item in treeView.Items)
            {

                if (item.Tag.Equals(TagMatch))
                {
                    tviState[i++] = item.IsExpanded;

                }
            }
        }



    }
}
