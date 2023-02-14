using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUS20S_Project2_1_진수현_201921093
{
    public partial class Form1 : Form
    {
        public int Total = 0; //결제 금액
        
        public Form1()
        {
            InitializeComponent();
            Text = "상품 관리 프로그램";

            dataGridView1.DataSource = DataMagager.VegetableList; //채소
            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;

            dataGridView2.DataSource = DataMagager.SeafoodList; //수산물
            dataGridView2.CurrentCellChanged += dataGridView2_CurrentCellChanged;

            dataGridView3.DataSource = DataMagager.IndustrialfoodList; //공산품
            dataGridView3.CurrentCellChanged += dataGridView3_CurrentCellChanged;

        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e) //채소
        {            
            try
            {  
                Vegetable vegetable_selected = dataGridView1.CurrentRow.DataBoundItem as Vegetable;
                textBox1.Text = vegetable_selected.Id;
                textBox2.Text = vegetable_selected.HousingDate;
                textBox3.Text = vegetable_selected.Name;
                textBox4.Text = string.Format("{0}", vegetable_selected.Price);
                textBox5.Text = string.Format("{0}", vegetable_selected.NumStock);
                checkBox1.Checked = vegetable_selected.isSoldOut;
            }
            catch (Exception)
            {
               
            }   
        }
        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e) //수산물
        {
            try
            {
                Seafood seafood_selected = dataGridView2.CurrentRow.DataBoundItem as Seafood;
                textBox21.Text = seafood_selected.Id;
                textBox20.Text = seafood_selected.Storage;
                dataGridView2.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
                textBox19.Text = seafood_selected.Name;
                textBox18.Text = string.Format("{0}", seafood_selected.Price);
                textBox17.Text = string.Format("{0}", seafood_selected.NumStock);
                checkBox2.Checked = seafood_selected.isSoldOut;
            }
            catch (Exception)
            {

            }
        }
        private void dataGridView3_CurrentCellChanged(object sender, EventArgs e) //공산품
        {
            try
            {
                Industrial Industrialfood_selected = dataGridView3.CurrentRow.DataBoundItem as Industrial;
                textBox31.Text = Industrialfood_selected.Id;
                textBox22.Text = Industrialfood_selected.ExpireDate;
                dataGridView3.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
                textBox30.Text = Industrialfood_selected.Name;
                textBox29.Text = string.Format("{0}", Industrialfood_selected.Price);
                textBox28.Text = string.Format("{0}", Industrialfood_selected.NumStock);
                checkBox3.Checked = Industrialfood_selected.isSoldOut;
            }
            catch (Exception)
            {

            }
        }
        private void button1_Click(object sender, EventArgs e) //채소 추가
        {
            try //데이터 형식 검사 
            {
                int.Parse(textBox4.Text);
                int.Parse(textBox5.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("숫자만 입력가능합니다.");
            }

            try
            {
                
                if (DataMagager.VegetableList.Exists((x) => x.Id == textBox1.Text))
                {
                    MessageBox.Show("이미 존재하는 상품입니다.");
                }
                else
                {
                 
                    Vegetable vegetable = new Vegetable()
                    { 
                        Id = textBox1.Text,
                        HousingDate = textBox2.Text,
                        Name = textBox3.Text,
                        Price = int.Parse(textBox4.Text),
                        NumStock = int.Parse(textBox5.Text),
                        isSoldOut = checkBox1.Checked
                    };
                    DataMagager.VegetableList.Add(vegetable);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataMagager.VegetableList;
                    DataMagager.Save();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e) //채소 수정
        {
            try
            {
                Vegetable vegetable = DataMagager.VegetableList.Single((x) => x.Id == textBox1.Text);
                vegetable.HousingDate = textBox2.Text;
                vegetable.Name = textBox3.Text;
                vegetable.Price = int.Parse(textBox4.Text);
                vegetable.NumStock = int.Parse(textBox5.Text);
                checkBox1.Checked = vegetable.NumStock > 0 ? false : true;
                vegetable.isSoldOut = checkBox1.Checked;

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataMagager.VegetableList;
                DataMagager.Save();

            }
            catch (Exception)
            {
                MessageBox.Show("존재하지 않는 상품입니다.");
            }
        }

        private void button3_Click(object sender, EventArgs e) //채소 삭제
        {
            try
            {
                Vegetable vegetable = DataMagager.VegetableList.Single((x) => x.Id == textBox1.Text);
                DataMagager.VegetableList.Remove(vegetable);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataMagager.VegetableList;
                DataMagager.Save();
            }
            catch
            {
                MessageBox.Show("존재하지 않는 상품입니다.");
            }
        }

        private void button4_Click(object sender, EventArgs e) //채소 담기
        {
            try
            {
                int quantity = int.Parse(textBox6.Text); //수량

                Vegetable basket = dataGridView1.CurrentRow.DataBoundItem as Vegetable;
                basket.Id = textBox1.Text;
                basket.HousingDate = textBox2.Text;
                basket.Name = textBox3.Text;
                basket.Price = int.Parse(textBox4.Text);
                basket.NumStock -= quantity;
                checkBox1.Checked = basket.NumStock > 0 ? false : true;
                basket.isSoldOut = checkBox1.Checked;

                textBox11.Text += "[" + basket.Id + "]" + basket.Name + string.Format("({0}개)", quantity) + Environment.NewLine; //장바구니 담기
                Total += basket.Price*quantity; //담은 품명의 금액 저장

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataMagager.VegetableList;
                DataMagager.Save();

            }
            catch (Exception)
            {
                MessageBox.Show("숫자만 입력가능합니다."); //데이터형식 검사
            }
        }

        private void button5_Click(object sender, EventArgs e) //채소 필터적용
        {
            try
            {
               
                int vpriceMin = int.Parse(textBox7.Text);
                int vpriceMax = int.Parse(textBox8.Text);
                int vnumStock = int.Parse(textBox9.Text);
                string housingDate = textBox10.Text;


                var v = from item in DataMagager.VegetableList
                        where item.Price >= vpriceMin && item.Price <= vpriceMax
                        where item.NumStock <= vnumStock
       //               where DateTime.Parse(item.HousingDate) <= DateTime.Parse(housingDate)
                        select item;

                List<Vegetable> Vegetable_Filtered = new List<Vegetable>();
                foreach (var item in v)
                    Vegetable_Filtered.Add(item as Vegetable);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Vegetable_Filtered;
            }
            catch (FormatException)
            {
                MessageBox.Show("숫자만 입력가능합니다."); //데이터형식 검사
            }
        }

        private void button6_Click(object sender, EventArgs e) //채소 필터해제
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataMagager.VegetableList;
        }
 
        private void button14_Click(object sender, EventArgs e) //수산물 추가
        {
            try
            {
                int.Parse(textBox18.Text);
                int.Parse(textBox17.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("숫자만 입력가능합니다.");
            }
            try
            {
                if (DataMagager.SeafoodList.Exists((x) => x.Id == textBox21.Text))
                {
                    MessageBox.Show("이미 존재하는 상품입니다.");
                }
                else
                {
                    Seafood seafood = new Seafood()
                    {
                        Id = textBox21.Text,
                        Storage = textBox20.Text,
                        Name = textBox19.Text,
                        Price = int.Parse(textBox18.Text),
                        NumStock = int.Parse(textBox17.Text),
                        isSoldOut = checkBox2.Checked
                    };
                    DataMagager.SeafoodList.Add(seafood);

                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = DataMagager.SeafoodList;
                    DataMagager.Save();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button13_Click(object sender, EventArgs e) //수산물 수정
        {
            try
            {
                Seafood seafood = DataMagager.SeafoodList.Single((x) => x.Id == textBox21.Text);
                seafood.Storage = textBox20.Text;
                seafood.Name = textBox19.Text;
                seafood.Price = int.Parse(textBox18.Text);
                seafood.NumStock = int.Parse(textBox17.Text);
                checkBox2.Checked = seafood.NumStock > 0 ? false : true;
                seafood.isSoldOut = checkBox2.Checked;

                dataGridView2.DataSource = null;
                dataGridView2.DataSource = DataMagager.SeafoodList;
                DataMagager.Save();

            }
            catch (Exception)
            {
                MessageBox.Show("존재하지 않는 상품입니다.");
            }

        }

        private void button12_Click(object sender, EventArgs e) //수산물 삭제
        {
            try
            {
                Seafood seafood = DataMagager.SeafoodList.Single((x) => x.Id == textBox21.Text);
                DataMagager.SeafoodList.Remove(seafood);

                dataGridView2.DataSource = null;
                dataGridView2.DataSource = DataMagager.SeafoodList;
                DataMagager.Save();
            }
            catch
            {
                MessageBox.Show("존재하지 않는 상품입니다.");
            }
        }

        private void button11_Click(object sender, EventArgs e) //수산물 담기
        {
            try
            {
                int quantity = int.Parse(textBox16.Text);

                Seafood basket1 = dataGridView2.CurrentRow.DataBoundItem as Seafood;
                basket1.Id = textBox21.Text;
                basket1.Storage = textBox20.Text;
                basket1.Name = textBox19.Text;
                basket1.Price = int.Parse(textBox18.Text);
                basket1.NumStock -= quantity; 
                checkBox2.Checked = basket1.NumStock > 0 ? false : true;
                basket1.isSoldOut = checkBox2.Checked;

                textBox11.Text += "[" + basket1.Id + "]" + basket1.Name + string.Format("({0}개)", quantity) + Environment.NewLine; 
                Total += basket1.Price * quantity; 

                dataGridView2.DataSource = null;
                dataGridView2.DataSource = DataMagager.SeafoodList;
                DataMagager.Save();

            }
            catch (Exception)
            {
                MessageBox.Show("숫자만 입력가능합니다.");
            }

        }

        private void button10_Click(object sender, EventArgs e) //수산물 필터적용
        {
            try
            {
                int spriceMin = int.Parse(textBox15.Text);
                int spriceMax = int.Parse(textBox14.Text);
                int snumStock = int.Parse(textBox13.Text);
                string storage = textBox12.Text;

                var v = from item in DataMagager.SeafoodList
                        where item.Price >= spriceMin && item.Price <= spriceMax
                        where item.NumStock <= snumStock
                        where item.Storage == storage
                        select item;

                List<Seafood> seafood_Filtered = new List<Seafood>();
                foreach (var item in v)
                    seafood_Filtered.Add(item as Seafood);

                dataGridView2.DataSource = null;
                dataGridView2.DataSource = seafood_Filtered;
            }
            catch (FormatException)
            {
                MessageBox.Show("숫자만 입력가능합니다.");
            }

        }

        private void button9_Click(object sender, EventArgs e) //수산물 필터해제
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = DataMagager.SeafoodList;

        }

        private void button20_Click(object sender, EventArgs e) //공산품 추가
        {
            try
            {
                int.Parse(textBox29.Text);
                int.Parse(textBox28.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("숫자만 입력가능합니다.");
            }
            try
            {
                if (DataMagager.IndustrialfoodList.Exists((x) => x.Id == textBox31.Text))
                {
                    MessageBox.Show("이미 존재하는 상품입니다.");
                }
                else
                {
                    Industrial industrial = new Industrial()
                    {
                        Id = textBox31.Text,
                        ExpireDate = textBox22.Text,
                        Name = textBox30.Text,
                        Price = int.Parse(textBox29.Text),
                        NumStock = int.Parse(textBox28.Text),
                        isSoldOut = checkBox3.Checked
                    };
                    DataMagager.IndustrialfoodList.Add(industrial);

                    dataGridView3.DataSource = null;
                    dataGridView3.DataSource = DataMagager.IndustrialfoodList;
                    DataMagager.Save();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button19_Click(object sender, EventArgs e)//공산품 수정
        {
            try
            {
                Industrial industrial = DataMagager.IndustrialfoodList.Single((x) => x.Id == textBox31.Text);
                industrial.ExpireDate= textBox22.Text;
                industrial.Name = textBox30.Text;
                industrial.Price = int.Parse(textBox29.Text);
                industrial.NumStock = int.Parse(textBox28.Text);
                checkBox3.Checked = industrial.NumStock > 0 ? false : true;
                industrial.isSoldOut = checkBox3.Checked;

                dataGridView3.DataSource = null;
                dataGridView3.DataSource = DataMagager.IndustrialfoodList;
                DataMagager.Save();

            }
            catch (Exception)
            {
                MessageBox.Show("존재하지 않는 상품입니다.");
            }

        }

        private void button18_Click(object sender, EventArgs e) //공산품 삭제
        {
            try
            {
                Industrial industrial = DataMagager.IndustrialfoodList.Single((x) => x.Id == textBox31.Text);
                DataMagager.IndustrialfoodList.Remove(industrial);

                dataGridView3.DataSource = null;
                dataGridView3.DataSource = DataMagager.IndustrialfoodList;
                DataMagager.Save();
            }
            catch
            {
                MessageBox.Show("존재하지 않는 상품입니다.");
            }
        }

        private void button17_Click(object sender, EventArgs e) // 공산품 담기
        {
            try
            {
                int quantity = int.Parse(textBox27.Text);

                Industrial basket2 = dataGridView3.CurrentRow.DataBoundItem as Industrial;
                basket2.Id = textBox31.Text;
                basket2.ExpireDate = textBox22.Text;
                basket2.Name = textBox30.Text;
                basket2.Price = int.Parse(textBox29.Text);
                basket2.NumStock -= quantity;
                checkBox3.Checked = basket2.NumStock > 0 ? false : true;
                basket2.isSoldOut = checkBox3.Checked;

                textBox11.Text += "[" + basket2.Id + "]" + basket2.Name + string.Format("({0}개)", quantity) + Environment.NewLine;
                Total += basket2.Price * quantity;

                dataGridView3.DataSource = null;
                dataGridView3.DataSource = DataMagager.IndustrialfoodList;
                DataMagager.Save();

            }
            catch (Exception)
            {
                MessageBox.Show("숫자만 입력가능합니다.");
            }
        }

        private void button16_Click(object sender, EventArgs e) //공산품 필터적용
        {
            try
            {
                int priceMin = int.Parse(textBox26.Text);
                int priceMax = int.Parse(textBox25.Text);
                int numStock = int.Parse(textBox24.Text);
                string expireDate = textBox23.Text;

                var v = from item in DataMagager.IndustrialfoodList
                        where item.Price >= priceMin && item.Price <= priceMax
                        where item.NumStock <= numStock
 //                     where item.ExpireDate 
                        select item;

                List<Industrial> industrial_Filtered = new List<Industrial>();
                foreach (var item in v)
                    industrial_Filtered.Add(item as Industrial);

                dataGridView3.DataSource = null;
                dataGridView3.DataSource = industrial_Filtered;
            }
            catch (FormatException)
            {
                MessageBox.Show("숫자만 입력가능합니다.");
            }
        }

        private void button15_Click(object sender, EventArgs e)//공산품 필터해제
        {
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = DataMagager.IndustrialfoodList;
        }
        private void button7_Click(object sender, EventArgs e) //결제
        {
            MessageBox.Show("판매완료:)\n" + "총 가격은 " + Total + " 원입니다.");
        }

        private void button8_Click(object sender, EventArgs e) //닫기 
        {
            this.Close();
        }

    }
}
