﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class NewHRTmpltMenuItem : BaseTreeMenuItem
    {
        public NewHRTmpltMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.HRTmplt");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 1
&& (TreeView.CurrentNode is TmpltRootNode || TreeView.CurrentNode is TmpltFolderNode))
                Visible = true;
            else
                Visible = false;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.NewTmplt(((BaseFolderElementNode)TreeView.CurrentNode).Element, TmpltType.Hr);
            base.OnClick(e);

        }
    }
}
