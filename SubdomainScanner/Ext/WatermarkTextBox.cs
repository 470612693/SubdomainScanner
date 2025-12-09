using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubdomainScanner
{
    public class WatermarkTextBox : TextBox
    {
        private string _watermark = "请输入内容...";
        private Color _watermarkColor = Color.Gray;
        private bool _isWatermarkVisible = true;

        public string Watermark
        {
            get => _watermark;
            set
            {
                _watermark = value;
                UpdateWatermarkVisibility();
                Invalidate();
            }
        }

        public Color WatermarkColor
        {
            get => _watermarkColor;
            set
            {
                _watermarkColor = value;
                Invalidate();
            }
        }

        public WatermarkTextBox()
        {
            this.TextChanged += (s, e) => UpdateWatermarkVisibility();
            this.Enter += (s, e) => ShowWatermark(false);
            this.Leave += (s, e) => ShowWatermark(true);
            UpdateWatermarkVisibility();
        }

        private void ShowWatermark(bool show)
        {
            if (show && string.IsNullOrWhiteSpace(Text))
            {
                _isWatermarkVisible = true;
                ForeColor = _watermarkColor;
            }
            else if (!show && Text == _watermark)
            {
                _isWatermarkVisible = true;
                Text = string.Empty;
                ForeColor = SystemColors.WindowText;
            }
            Invalidate();
        }

        private void UpdateWatermarkVisibility()
        {
            _isWatermarkVisible = string.IsNullOrWhiteSpace(Text) &&
                                 !this.Focused &&
                                 Text != _watermark;
            ForeColor = _isWatermarkVisible ? _watermarkColor : SystemColors.WindowText;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_isWatermarkVisible)
            {
                using (var brush = new SolidBrush(_watermarkColor))
                {
                    var rect = ClientRectangle;
                    rect.Inflate(-2, -2);
                    e.Graphics.DrawString(
                        _watermark,
                        Font,
                        brush,
                        rect,
                        new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        });
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            // 禁止默认文本被选中
            base.WndProc(ref m);
            if (m.Msg == 0x000F) // WM_PAINT
            {
                if (_isWatermarkVisible)
                {
                    using (var graphics = CreateGraphics())
                    {
                        var rect = ClientRectangle;
                        rect.Inflate(-2, -2);
                        TextRenderer.DrawText(
                            graphics,
                            _watermark,
                            Font,
                            rect,
                            _watermarkColor,
                            TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                    }
                }
            }
        }
    }
}
