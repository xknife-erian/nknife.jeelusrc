using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class BaseMediaCodeForm : BaseForm
    {

        FlashInfo flashInfo = null;
        decimal ratio = 0;

        public decimal Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }
        public virtual MediaFileType MediaType { get;set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseMediaCodeForm()
        {
            InitializeComponent();
            GetCHSTextforInsertMedia getCHSforIn = new GetCHSTextforInsertMedia();
            string[] Quality = getCHSforIn.FlashQuality;
            string[] calign = getCHSforIn.Align;
            string[] Scale = getCHSforIn.Scale;
            string[] Unit = getCHSforIn.Unit;

            foreach (string Q in Quality)
                qualityComboBox.Items.Add(Q);
            foreach (string a in calign)
                alignComboBox.Items.Add(a);
            foreach (string sc in Scale)
                scaleComboBox.Items.Add(sc);
            foreach (string u in Unit)
            {
                widthUintComboBox.Items.Add(u);
                heightUintComboBox.Items.Add(u);
            }

            InitControls();

            this.ImeMode = ImeMode.On;

            this.widthNumUpDown.Validated += new EventHandler(widthNumUpDown_Validated);
            this.heightNumUpDown.Validated += new EventHandler(heightNumUpDown_Validated);
        }

        public virtual void heightNumUpDown_Validated(object sender, EventArgs e)
        {
            
        }
        public virtual void widthNumUpDown_Validated(object sender, EventArgs e)
        {         
        }

        public virtual void medioPathBtn_Click(object sender, EventArgs e)
        {
        }

        public void SetFormedioPathChange(MediaFileType mediaType)
        {
            string resourceId = SiteResourceService.SelectResource(mediaType, this);
            if (resourceId != null)
            {
                SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
                FileSimpleExXmlElement fileEle = doc.GetElementById(resourceId) as FileSimpleExXmlElement;
                pathTextBox.Text = fileEle.AbsoluteFilePath;
                try
                {
                    flashInfo = new FlashInfo(fileEle.AbsoluteFilePath);
                }
                catch
                { }
                if (flashInfo != null)
                {
                    widthNumUpDown.Value = flashInfo.Width;
                    heightNumUpDown.Value = flashInfo.Height;
                    ratio = widthNumUpDown.Value / heightNumUpDown.Value;
                    limitScaleCheckBox.Visible = true;
                    limitScaleCheckBox.Enabled = true;
                }
                else
                {
                    widthNumUpDown.Value = 100;
                    heightNumUpDown.Value = 100;
                    limitScaleCheckBox.Visible = false;

                }
                widthUintComboBox.SelectedIndex = heightUintComboBox.SelectedIndex = 0;
                
                //设置窗体的一些默认值
                this.widthCheckBox.Checked = true;
                this.heightCheckBox.Checked = true;
                this.loopCheckBox.Checked = true;
                this.autoPlayCheckBox.Checked = true;

                this.MediaID = resourceId;
            }
        }

        /// <summary>
        /// 取得文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string getFileName(string fileName)
        {
            string file_Name = "";
            int leng = fileName.Length;
            int lastP = fileName.LastIndexOf(@"\") + 1;
            file_Name = fileName.Substring(lastP, leng - lastP);
            return file_Name;
        }

        private void resizeButton_Click(object sender, EventArgs e)
        {
            InitControls();
        }

        void InitControls()
        {
            widthCheckBox.Checked = true;
            heightCheckBox.Checked = true;
            limitScaleCheckBox.Enabled = true;
            limitScaleCheckBox.Checked = true;
            if (flashInfo != null)
            {
                widthNumUpDown.Value = flashInfo.Width;
                heightNumUpDown.Value = flashInfo.Height;
            }
            else
            {
                widthNumUpDown.Value = 100;
                heightNumUpDown.Value = 100;
            }
            widthUintComboBox.SelectedIndex = heightUintComboBox.SelectedIndex = 0;

            qualityComboBox.SelectedIndex = 0;
            alignComboBox.SelectedIndex = 0;
            scaleComboBox.SelectedIndex = 0;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            pathTextBox.Clear();
            idTextBox.Clear();
            titleTextBox.Clear();
            accessKeyTextBox.Clear();
            tabTextBox.Clear();

            widthCheckBox.Checked = true;
            heightCheckBox.Checked = true;
            limitScaleCheckBox.Enabled = true;
            limitScaleCheckBox.Checked = true;
            widthNumUpDown.Value = 100;
            heightNumUpDown.Value = 100;
            widthUintComboBox.SelectedIndex = heightUintComboBox.SelectedIndex = 0;

        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            DesignDataXmlDocument.InsertMediaData mediaData = new DesignDataXmlDocument.InsertMediaData();
            mediaData.MediaAlign = this.alignComboBox.SelectedIndex;
            mediaData.MediaHMargin = this.hspaceNumUpDown.Value;
            mediaData.MediaQuality = this.qualityComboBox.SelectedIndex;
            mediaData.MediaScale = this.scaleComboBox.SelectedIndex;
            mediaData.MediaVMargin = this.vspaceNumUpDown.Value;

            Service.Sdsite.DesignDataDocument.SetHTMLDesignerInsertMediaData(MediaType, mediaData);
        }


        private void BaseMediaCodeForm_Load(object sender, EventArgs e)
        {
            DesignDataXmlDocument.InsertMediaData mediaData = Service.Sdsite.DesignDataDocument.GetHTMLDesignerInsertMediaData(MediaType);
            if (mediaData != null)
            {
                hspaceNumUpDown.Value = mediaData.MediaHMargin;
                vspaceNumUpDown.Value = mediaData.MediaVMargin;
                alignComboBox.SelectedIndex = mediaData.MediaAlign;
                qualityComboBox.SelectedIndex = mediaData.MediaQuality;
                scaleComboBox.SelectedIndex = mediaData.MediaScale;
            }

        }

        #region 对外抛出属性

        public string MediaID { get; set; }
        public string MediaScale
        {
            get
            {
                string scale = "";
                switch (scaleComboBox.SelectedIndex)
                {
                    case 1: scale = "noborder"; break;
                    case 2: scale = "exactfit"; break;
                }
                return scale;
            }
            set { scaleComboBox.Text = value; }
        }
        public decimal MediaVspace
        {
            get { return vspaceNumUpDown.Value; }
            set { vspaceNumUpDown.Value = value; }
        }
        public string MediaHeigUint
        {
            get
            {
                if (heightUintComboBox.Text == heightUintComboBox.Items[1].ToString())
                    return "%";
                else
                    return "px";
            }
            set { heightUintComboBox.Text = value; }
        }
        public decimal MediaHeight
        {
            get { return heightNumUpDown.Value; }
            set { heightNumUpDown.Value = value; }
        }
        public decimal MediaHspace
        {
            get { return hspaceNumUpDown.Value; }
            set { hspaceNumUpDown.Value = value; }
        }
        public string MediaWidUint
        {
            get
            {
                if (widthUintComboBox.Text == widthUintComboBox.Items[1].ToString())
                    return "%";
                else
                    return "px";
            }
            set { widthUintComboBox.Text = value; }
        }
        public decimal MediaWidth
        {
            get { return widthNumUpDown.Value; }
            set { widthNumUpDown.Value = value; }
        }
        public string MediaTab
        {
            get { return tabTextBox.Text; }
            set { tabTextBox.Text = value; }
        }
        public string MediaAccessKey
        {
            get { return accessKeyTextBox.Text; }
            set { accessKeyTextBox.Text = value; }
        }
        public string MediaTitle
        {
            get { return titleTextBox.Text; }
            set { titleTextBox.Text = value; }
        }
        public string MediaPath
        {
            get { return pathTextBox.Text; }
            set { pathTextBox.Text = value; }
        }
        public bool MediaAutoPlay
        {
            get { return autoPlayCheckBox.Checked; }
            set { autoPlayCheckBox.Checked = value; }
        }
        public bool MediaLoop
        {
            get { return loopCheckBox.Checked; }
            set { loopCheckBox.Checked = value; }
        }
        #endregion

    }
}