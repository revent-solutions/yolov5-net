﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Yolov5Net.Scorer;
using Yolov5Net.Scorer.Models;

namespace Yolov5Net.App
{
    class Program
    {
        static void Main(string[] args)
        {
            using var image = Image.FromFile("Assets/test.jpg");

            // 한번만 선언.
            using var scorer = new YoloScorer<YoloCocoP5Model>("Assets/Weights/yolov5s.onnx");

            // prediction
            List<YoloPrediction> predictions = scorer.Predict(image);

            using var graphics = Graphics.FromImage(image);

            // 결과 그리기
            foreach (var prediction in predictions) 
            {
                double score = Math.Round(prediction.Score, 2);

                graphics.DrawRectangles(new Pen(prediction.Label.Color, 1),
                    new[] { prediction.Rectangle });

                var (x, y) = (prediction.Rectangle.X - 3, prediction.Rectangle.Y - 23);

                graphics.DrawString($"{prediction.Label.Name} ({score})",
                    new Font("Consolas", 16, GraphicsUnit.Pixel), new SolidBrush(prediction.Label.Color),
                    new PointF(x, y));
            }

            image.Save("Assets/result.jpg");
        }
    }
}
