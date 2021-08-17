using System;

namespace Shapes
{

    public abstract class Shape
    {
        // area for each shape is different
        public abstract float Area();
    }

    public class Circle : Shape
    {
        private float radius;

        // constructors
        public Circle() { }
        public Circle(float r)
        {
            radius = r;
        }

        public float GetRadius()
        {
            return radius;
        }
        public void SetRadius(float r)
        {
            radius = r;
        }

        public override float Area()
        {
            return radius * radius * (float)Math.PI;
        }
    }

    public class Rectangle : Shape
    {
        private float width, height;

        // constructors
        public Rectangle() { }
        public Rectangle(float w, float h)
        {
            width = w;
            height = h;
        }

        // getters
        public float GetWidth()
        {
            return width;
        }
        public float GetHeight()
        {
            return height;
        }

        // setters
        public void SetWidth(float w)
        {
            width = w;
        }
        public void SetHeight(float h)
        {
            height = h;
        }

        public override float Area()
        {
            return width * height;
        }
    }

    public class Triangle : Shape
    {
        private float sideA, sideB, sideC;

        // constructors
        public Triangle() { }
        public Triangle(float a, float b, float c)
        {
            sideA = a;
            sideB = b;
            sideC = c;
        }

        // getters
        public float GetSideA()
        {
            return sideA;
        }
        public float GetSideB()
        {
            return sideB;
        }
        public float GetSideC()
        {
            return sideC;
        }

        // setters
        public void SetSideA(float a)
        {
            sideA = a;
        }
        public void SetSideB(float b)
        {
            sideB = b;
        }
        public void SetSideC(float c)
        {
            sideC = c;
        }

        public override float Area()
        {
            float s = (sideA + sideB + sideC) / 2;
            return (float)Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));

        }
    }

    class Program
    {
        static void CreateCircle()
        {
            float radius;
            string radiusString;

            Console.Write("Enter circle radius: ");
            radiusString = Console.ReadLine();
            radius = float.Parse(radiusString);
            if(radius<=0)
            {
                Console.WriteLine("Radius must be positive.");
                return;
            }

            Circle circle = new Circle(radius);
            Console.WriteLine("Circle created!");

            Console.WriteLine("Circle's area is " + circle.Area());
        }

        static void CreateRectangle()
        {
            float width, height;
            string widthString, heightString;

            Console.Write("Enter rectangle width: ");
            widthString = Console.ReadLine();
            Console.Write("Enter rectangle height: ");
            heightString = Console.ReadLine();
            width = float.Parse(widthString);
            height = float.Parse(heightString);
            if (width<=0 || height <= 0)
            {
                Console.WriteLine("Width and height must be positive.");
                return;
            }

            Rectangle rectangle = new Rectangle(width, height);
            Console.WriteLine("Rectangle created!");

            Console.WriteLine("Rectangle's area is " + rectangle.Area());
        }

        static void CreateTriangle()
        {
            float sideA, sideB, sideC;
            string aString, bString, cString;

            Console.Write("Enter triangle side a: ");
            aString = Console.ReadLine();
            Console.Write("Enter triangle side b: ");
            bString = Console.ReadLine();
            Console.Write("Enter triangle side c: ");
            cString = Console.ReadLine();
            sideA = float.Parse(aString);
            sideB = float.Parse(bString);
            sideC = float.Parse(cString);
            if (sideA <= 0 || sideB <= 0 || sideC <= 0)
            {
                Console.WriteLine("Sides must be positive.");
                return;
            }

            Triangle triangle = new Triangle(sideA, sideB, sideC);
            Console.WriteLine("Triangle created!");

            Console.WriteLine("Triangle's area is " + triangle.Area());
        }

        static void Main(string[] args)
        {
            char c;
            string readFromUser;
            do
            {
                Console.WriteLine("Which shape would you like to create?");
                Console.WriteLine("Type 'c' for circle, 'r' for rectangle, 't' for triangle, or press 'x' for exit");
                readFromUser = Console.ReadLine();
                c = char.Parse(readFromUser);

                switch (c)
                {
                    case 'c':
                        CreateCircle();
                        break;
                    case 'r':
                        CreateRectangle();
                        break;
                    case 't':
                        CreateTriangle();
                        break;
                    case 'x':
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (c != 'x');
            
        }
    }
}