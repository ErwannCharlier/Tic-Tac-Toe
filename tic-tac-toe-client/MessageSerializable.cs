using System.Text.Json;

namespace tictactoe_interface
{
    public class MessageSerializable
    {
        public int Col { get; set; }
        public int Row { get; set; }

        public bool HasError { get; set; }

        public MessageSerializable(int col, int row, bool hasError) { 
            Col = col; 
            Row = row; 
            HasError = hasError;
        }    

        public string CreateJSONSerialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static MessageSerializable ReadJSONSerialize(string jsonString)
        {

            return JsonSerializer.Deserialize<MessageSerializable>(jsonString);
        }


    }
}
