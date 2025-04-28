

namespace PoleChudes
{
    public class Baraban : GraphicsView, IDrawable
    {
        private BarabanViewModel _viewModel;

        public Baraban(BarabanViewModel viewModel)
        {
            _viewModel = viewModel;
            Drawable = this;

            // Подписка на изменения угла
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(BarabanViewModel.Angle))
                {
                    Invalidate(); // Перерисовать барабан
                }
            };
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            float centerX = (float)(dirtyRect.Width / 2);
            float centerY = (float)(dirtyRect.Height / 2);
            float radius = Math.Min(centerX, centerY) - 10;

            canvas.Translate(centerX, centerY);
            canvas.Rotate((float)_viewModel.Angle);

            // Рисуем круг с 9 секторами
            int sectorCount = 9;
            float sectorAngle = 360f / sectorCount;

            for (int i = 0; i < sectorCount; i++)
            {
                canvas.SaveState();
                canvas.Rotate(sectorAngle * i);

                // Нарисовать сектор
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawLine(0, 0, radius, 0);

                canvas.RestoreState();
            }

            // Нарисовать окружность
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 4;
            canvas.DrawCircle(0, 0, radius);

            canvas.RestoreState();
        }
    }
}
