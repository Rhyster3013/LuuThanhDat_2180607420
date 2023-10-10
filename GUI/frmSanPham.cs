using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De02.GUI
{
    public partial class frmSanPham : Form
    {
        Function func;
        public frmSanPham()
        {
            InitializeComponent();
            func = new Function(this);
        }

        #region form load & button enable

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            func.loadFrm();
        }

        private void lsvSP_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                func.cellclick();
                btnAdd.Enabled = false;
            }
            catch
            {
                func.loadFrm();
            }
        }

        private void txbID_TextChanged(object sender, EventArgs e)
        {
            func.checknull();
            func.checkID(txbID.Text);

            if (txbID.TextLength > 6)
            {
                DialogResult tb;
                tb = (MessageBox.Show("Nhập Mã quá dài", "NHẬP LẠI", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                txbID.Clear();
            }
        }

        private void txbName_TextChanged(object sender, EventArgs e)
        {
            func.checknull();
            func.checkID(txbID.Text);

            if (txbName.TextLength > 30)
            {
                DialogResult tb;
                tb = (MessageBox.Show("Nhập Tên quá dài", "NHẬP LẠI", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                txbName.Clear();
            }
        }

        #endregion


        #region button click

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                func.updSP();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                func.loadFrm();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessage = "Lỗi validation: ";
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        errorMessage += validationError.ErrorMessage + "\n";
                    }
                }
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            try
            {
                func.updSP();
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                func.loadFrm();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessage = "Lỗi validation: ";
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        errorMessage += validationError.ErrorMessage + "\n";
                    }
                }
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            func.clearFrm();
        }

        private void frmSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn đóng?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(rs == DialogResult.No)
                e.Cancel = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn xóa " + txbName.Text + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(rs == DialogResult.Yes)
                func.delSP();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txbFind.Text == "")
                func.loadFrm();
            else
                func.find(txbFind.Text);
        }

        #endregion
    }
}
