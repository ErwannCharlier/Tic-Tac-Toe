 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            string jsonString = JsonSerializer.Serialize(this);
            return jsonString;
        }

        public static MessageSerializable ReadJSONSerialize(string jsonString)
        {

            MessageSerializable maClasseDeserialized = JsonSerializer.Deserialize<MessageSerializable>(jsonString);
            return maClasseDeserialized;
        }
        public override string ToString()
        {
            return $"{Row} {Col} {HasError} ";
        }
    }
}
