namespace SimulationGUI;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        Label label1 = new Label()
        {
            Text = "&Speed",
            Location = new Point(10, 10),
            TabIndex = 0
        };

        TextBox field1 = new TextBox()
        {
            Location = new Point(label1.Location.X, label1.Bounds.Bottom + Padding.Top),
            TabIndex = 1
        };
        Label label2 = new Label()
        {
            Text = "&Wattage",
            Location = new Point(150, 10),
            TabIndex = 2
        };

        TextBox field2 = new TextBox()
        {
            Location = new Point(label2.Location.X, label2.Bounds.Bottom + Padding.Top),
            TabIndex = 3
        };
        
        Label label3 = new Label()
        {
            Text = "&Wattage",
            Location = new Point(290, 10),
            TabIndex = 4
        };

        TextBox field3 = new TextBox()
        {
            Location = new Point(label3.Location.X, label3.Bounds.Bottom + Padding.Top),
            TabIndex = 5
        };
        
        Label label4 = new Label()
        {
            Text = "&Wattage",
            Location = new Point(430, 10),
            TabIndex = 6
        };

        TextBox field4 = new TextBox()
        {
            Location = new Point(label4.Location.X, label4.Bounds.Bottom + Padding.Top),
            TabIndex = 7
        };
        
       
        Controls.Add(label1);
        Controls.Add(field1);
        
        Controls.Add(label2);
        Controls.Add(field2);
        
        Controls.Add(label3);
        Controls.Add(field3);
        
        Controls.Add(label4);
        Controls.Add(field4);
        
    }
}