$(document).ready(function () {

	var api = "https://localhost:49163/api/Currency";

	$("#btnBuscarCotacao").click(function (e) {
		e.preventDefault();

		$("#divCotacao").hide();

		var moedasSelecionadas = [];
		$.each($("input[name='moedas']:checked"), function () {
			moedasSelecionadas.push($(this).val());
		});

		if (moedasSelecionadas.length == 0) {
			alert('Selecione uma ou mais moedas para ver a conversão!');
			return;
		}

		var moedas = "";
		$.each($(moedasSelecionadas), function () {
			moedas += this.toString() + ',';
		});
		moedas = moedas.substring(0, moedas.length - 1);

		var source = "USD";
		var url = api + "?source=" + source + "&currencyFor=" + moedas;

		$.ajax({
			type: "GET",
			contentType: "application/json",
			url: url,
			success: function (d) {
				var html = "<div style='color:green;'><h4>Cotação Atual</h4>"
				$.each(d, function (key, value) {
					html += "<p>" + key + ": " + value + "</p>";
				});
				html += "</div>"
				$("#divCotacao").html(html);
				$("#divCotacao").fadeIn("fast");
			},
			error: function (d) {
				var html = "<div style='color:#FF0000;'><h4>Erro!</h4><p>Cotação indisponível. Por favor tente novamente.</p></div>"
				$("#divCotacao").html(html);
				$("#divCotacao").fadeIn("fast");
			}
		});
	});

});