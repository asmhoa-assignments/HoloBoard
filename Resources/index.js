var fs = require('fs');
var request = require('request');
var path = require('path');

fs.readFile('strokes.txt', 'utf8',function(err,file){
  //call eq.php with string
  request.post(
    {url: 'http://cat.prhlt.upv.es/mer/eq.php', form:{strokes:file}},
    (err, response, body) => {
     fs.writeFile(path.join(__dirname, "logBody.json"), JSON.stringify(response), "utf8", function(err) {
        if(err) {
            return console.log(err);
        }
        console.log("The file was saved!");
    }); 
      var url = `http://api.wolframalpha.com/v2/query?appid=YU4XG4-6KYLKUQ9KQ&input=${encodeURIComponent(body)}&format=image&includepodid=Solution`;
      fs.writeFile(path.join(__dirname, "log.txt"), url, "utf8", function(err) {
          if(err) {
              return console.log(err);
          }
          console.log("The file was saved!");
      }); 
    });

  //var scg = strokesToScg(JSON.parse(file))
  // fs.writeFile('strokes.scgink', );

})
function strokesToScg(strokes) {
  var scg = 'SCG_INK\n' + strokes.length + '\n'
  strokes.forEach(function (stroke) {
  //  scg += stroke.length + '\n'
    stroke.forEach(function (p) {
      scg += p[0] + ' ' + p[1] + '\n'
    })
  })

  return scg
}
