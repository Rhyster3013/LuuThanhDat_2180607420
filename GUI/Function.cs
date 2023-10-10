using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using BLL;
using DAL.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace De02.GUI
{
    internal class Function
    {
        frmSanPham main;

        private readonly bllSanPham bllS = new bllSanPham();
        private readonly bllLoaiSP bllC = new bllLoaiSP();

        public Function(frmSanPham f)
        {
            main = f;
        }

        #region form load

        public void loadFrm()
        {
            var listS = bllS.GetAll();
            var listC = bllC.GetAll();

            getcb(listC);
            getLV(listS);

            clearFrm();
            clearBtn();
        }

        public void getLV(List<SanPham> list)
        {
            main.lsvSP.Items.Clear();
            foreach (var item in list)
            {
                ListViewItem lvi = new ListViewItem(item.MaSP);

                lvi.SubItems.Add(item.TenSP);
                lvi.SubItems.Add(item.NgayNhap.ToString());
                lvi.SubItems.Add(item.LoaiSP.TenLoai);

                main.lsvSP.Items.Add(lvi);
            }
        }

        public void getcb(List<LoaiSP> list)
        {
            main.cbCategory.DataSource = list;
            main.cbCategory.DisplayMember = "TenLoai";
            main.cbCategory.ValueMember = "MaLoai";
        }

        public void clearFrm()
        {
            main.cbCategory.SelectedIndex = 0;
            main.txbFind.Clear();
            main.txbID.Clear();
            main.txbName.Clear();
            main.dtpDate.Value = DateTime.Today;
        }

        public void clearBtn()
        {
            main.btnAdd.Enabled = false;
            main.btnUpd.Enabled = false;
            main.btnDel.Enabled = false;
            main.btnSave.Enabled = false;
            main.btnClear.Enabled = true;
        }

        public void checknull()
        {
            if (main.txbID.Text != "" && main.txbName.Text != "")
            {
                main.btnAdd.Enabled = true;
                main.btnUpd.Enabled = true;
                main.btnDel.Enabled = true;
            }
            else
                clearBtn();
        }

        public void checkID(string s)
        {
            ItemModel ctx = new ItemModel();
            var id = bllS.loadFind(s,ctx);
            if (id != null)
            {
                main.btnAdd.Enabled = false;
            }
        }

        #endregion

        #region button click

        public void cellclick()
        {
            ListViewItem lvi = main.lsvSP.SelectedItems[0];

            string index = lvi.SubItems[0].Text;

            main.txbID.Text = lvi.SubItems[0].Text;
            main.txbName.Text = lvi.SubItems[1].Text;
            main.dtpDate.Text = lvi.SubItems[2].Text;
            main.cbCategory.Text = lvi.SubItems[3].Text;
        }

        public void updSP()
        {
            SanPham s = new SanPham();

            s.MaSP = main.txbID.Text.ToString();
            s.TenSP = main.txbName.Text.ToString();
            s.NgayNhap = main.dtpDate.Value;
            s.MaLoai = main.cbCategory.SelectedValue.ToString();

            bllS.Add(s);
        }
        
        public void delSP()
        {
            ItemModel ctx = new ItemModel();
            SanPham s = bllS.loadFind(main.txbID.Text, ctx);
            if (s != null)
            {
                bllS.Del(s, ctx);
                MessageBox.Show("Đã xóa sản phẩm");
                loadFrm();
            }
            else
            {
                MessageBox.Show("Sản phẩm cần xóa không tồn tại");
            }
        }
        public void find(string s)
        {
            ItemModel ctx = new ItemModel();
            var listRS = bllS.GetFind(s, ctx);
            getLV(listRS);
        }

        #endregion

    }
}
