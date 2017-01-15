var fs = require('fs');
var request = require('request');

fs.readFile('strokes.txt', 'utf8',function(err,file){
  //call eq.php with string
  request.post(
    {url: 'http://cat.prhlt.upv.es/mer/eq.php', form:{strokes:file}},
    (err, response, body) => {
      console.log(body);
      var url = `http://api.wolframalpha.com/v2/query?appid=YU4XG4-6KYLKUQ9KQ&input=${ encodeURIComponent(body)}&format=image&includepodid=Solution`;
      console.log(url);
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
