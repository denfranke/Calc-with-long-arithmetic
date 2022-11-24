using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class сравнение : Form
    {
        public сравнение()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            longint a = new longint(t1.Text);
            longint b = new longint(t2.Text);

            if (a < b)
                t3.Text = "<";
            else if (a > b)
                t3.Text = ">";
            else
                t3.Text = "==";

            longint k = a + b;
            t4.Text = k.get();

            if (a > b || (!(a < b) && !(a > b)))
            {
                k = a - b;
                t5.Text = k.get();
            }
            else
                if (a < b)
            {
                k = b - a;
                string s = "-" + k.get();
                t5.Text = s;
            }

            k = a * b;
            t6.Text = k.get();

            k = b / a;
            while (k.Count() > 0 && k.getd(0) == 0)
                k.DelAt(0);
            t7.Text = k.getrev();

            k = a.sqrt1();
            t8.Text = k.get();
        }
    }

    public class longint
    {
        List<int> digit = new List<int>();

        bool neg = false;

        public longint(longint a, longint b)
        {
            for (int i = a.Count() + b.Count(); i >= 0; i--)
                digit.Add(0);
        }

        public longint() { }

        public longint(string s)
        {
            if (s[0] != '-')
            {
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    digit.Add(s[i] - '0');
                }
                neg = false;
            }
            else
            {
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    digit.Add(s[i] - '0');
                }
                neg = true;
            }
        }

        public string get()
        {
            string ans = "";
            for (int i = digit.Count() - 1; i >= 0; i--)
            {
                ans += Convert.ToString(digit[i]);
            }
            return ans;
        }

        public string getrev()
        {
            string ans = "";
            for (int i = 0; i < digit.Count(); i++)
            {
                ans += Convert.ToString(digit[i]);
            }
            return ans;
        }

        public int Count()
        {
            return digit.Count;
        }

        public void Add(int k)
        {
            digit.Add(k);
        }

        public int getd(int i)
        {
            if (i < digit.Count())
                return digit[i];
            return 0;
        }

        public void setd(int i, int k)
        {
            this.digit[i] = k;
        }

        public void DelAt(int i)
        {
            digit.RemoveAt(i);
        }

        public longint reverse()
        {
            longint a1 = new longint();
            for (int i = digit.Count() - 1; i >= 0; i--)
                a1.Add(digit[i]);
            return a1;
        }

        public longint sqrt1()
        {
            longint a = this.copy();
            a = a.reverse();
            longint r = this.copy();
            r = r.reverse();
            longint l = new longint("0");
            longint one = new longint("1");
            longint two = new longint("2");
            while (l < r)
            {
                longint m = (r + l).reverse() / two;
                if (m * m < a)
                    l = (m + one);
                else
                    r = m.copy();
            }
            if (r * r < a && r * r == a)
                return r;
            return l;
        }

        public longint copy()
        {
            longint a1 = new longint();
            for (int i = 0; i < this.Count(); i++)
                a1.Add(this.getd(i));
            return a1;
        }

        public static longint operator +(longint a, longint b)
        {
            longint res = new longint();

            int r = 0;
            for (int i = 0; i < Math.Max(a.Count(), b.Count()); i++)
            {
                if (i < a.Count()) r += a.getd(i);
                if (i < b.Count()) r += b.getd(i);
                res.Add(r % 10);
                r /= 10;
            }
            if (r > 0)
                res.Add(r);
            return res;
        }

        public static longint operator -(longint a, longint b)
        {
            longint res = new longint(a.get());
            int r = 0;
            for (int i = 0; i < b.Count() || r > 0; i++)
            {
                if (i >= res.Count())
                    res.Add(0);
                if (i < b.Count())
                    res.digit[i] -= b.digit[i];
                if (res.digit[i] < 0 && r == 0)
                {
                    res.digit[i] += 10;
                    r = 1;
                    continue;
                }

                if (r > 0)
                {
                    if (res.digit[i] > 0)
                    {
                        res.digit[i]--;
                        r--;
                    }
                    else
                        res.digit[i] += 9;
                }
            }
            while (res.Count() > 1 && res.getd(res.Count() - 1) == 0)
                res.DelAt(res.Count() - 1);
            return res;
        }

        public static longint operator *(longint a, longint b)
        {
            longint c = new longint(a, b);
            for (int i = 0; i < a.Count(); i++)
            {
                for (int j = 0, carry = 0; j < b.Count() || carry != 0; j++)
                {
                    Int64 sum;
                    if (j < b.Count())
                        sum = c.getd(i + j) + a.getd(i) * b.getd(j) + carry;
                    else
                        sum = c.getd(i + j) + a.getd(i) * 0 + carry;
                    carry = (int)sum / 10;
                    c.setd(i + j, (int)sum % 10);
                }
            }
            while (c.Count() > 1 && c.getd(c.Count() - 1) == 0)
                c.DelAt(c.Count() - 1);
            return c;
        }

        public static longint operator /(longint a, longint b)
        {
            longint c = new longint();
            longint res = new longint();
            a = a.reverse();
            b = b.reverse();
            int i = 0;
            while (a.Count() > i)
            {
                longint d = new longint();
                int x = 0;
                while (i < a.Count() && c.reverse() < b.reverse())
                {
                    c.Add(a.getd(i));
                    if (x > 0)
                        res.Add(0);
                    x++;
                    i++;
                }
                while (c.Count() > 0 && c.getd(0) == 0)
                    c.DelAt(0);
                int k = 0;
                while (d.reverse() < c.reverse())
                {
                    d = (d.reverse() + b.reverse()).reverse();
                    k++;
                }
                if ((d.reverse() < c.reverse()) || (d.reverse() > c.reverse()))
                {
                    d = (d.reverse() - b.reverse()).reverse();
                    k--;
                }
                res.Add(k);
                //  return res;
                if (i == a.Count() && c.reverse() < b.reverse())
                    break;
                c = (c.reverse() - d.reverse()).reverse();
                while (c.Count() > 0 && c.getd(0) == 0)
                    c.DelAt(0);
            }
            return res;
        }

        public static bool operator <(longint a, longint b)
        {
            while (a.Count() > 0 && a.getd(a.Count() - 1) == 0)
                a.DelAt(0);
            while (b.Count() > 0 && b.getd(b.Count() - 1) == 0)
                b.DelAt(0);
            if (a.Count() != b.Count())
                return a.Count() < b.Count();
            for (int i = a.Count() - 1; i >= 0; --i)
            {
                if (a.digit[i] != b.digit[i])
                    return a.digit[i] < b.digit[i];
            }
            return false;
        }

        public static bool operator >(longint a, longint b)
        {
            return b < a;
        }
    }
}

