using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
/// <summary>
/// Classe para operar o carregamento de arquivo XML
/// apenas para facilitar o acesso aos nodes
/// </summary>

public class LoadXML
{
    private XmlDocument xmldoc = new XmlDocument();
    private XmlNode xmlroot;
    private string nodeName;
    private string attributeId;

    /// <summary>
    /// Metodo de inicialização do corregamento de arquivo XML
    /// </summary>
    /// <param name="xmlfile">path para o arquivo a ser carregado</param>
    /// 
    public LoadXML(string xmlfile)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("XML/"+ xmlfile);
        xmldoc.LoadXml(textAsset.text);
        //xmldoc.Load("/XML/" + xmlfile);
        xmlroot = xmldoc.DocumentElement;
    }
    /// <summary>
    /// Metodo que facilita o acesso de nodes com id
    /// </summary>
    /// <param name="idde">nome do campo do node que corresponde a id do node</param>
    /// 
    public void SetAttributeId(string idde)
    {
        attributeId = idde;
    }
    /// <summary>
    /// Metodo que cafilira o acesso de nodes com name
    /// </summary>
    /// <param name="nodename">nome do node correspondente aos itens</param>
    ///
    public void SetNodeName(string nodename)
    {
        nodeName = nodename;
    }
    /// <summary>
    /// Metodo que busca uma lista de nodes pelo nome
    /// </summary>
    /// <param name="node">Nome do node</param>
    /// <returns>uma lista de nodes correspondentes aos nome</returns>
    /// 
    public XmlNodeList GetNodes(string node)
    {
        XmlNodeList nodeList = xmlroot.SelectNodes(node);
        return nodeList;
    }
    /// <summary>
    /// Metodo que busca o primeiro resultado com um nome
    /// </summary>
    /// <param name="node">Nome do node</param>
    /// <returns>Os valores correspondente ao primeiro node encontrado</returns>
    /// 
    public XmlNode GetNode(string node)
    {
        XmlNodeList nodes = xmlroot.SelectNodes(node);
        return nodes[0];
    }
    /// <summary>
    /// Metodo que retorna o primeiro resultado com um nome e um atributo especifico
    /// </summary>
    /// <param name="node">Nome do Node</param>
    /// <param name="attr">Nome do Atributo</param>
    /// <returns>Os valores correspondente ao primeiro node encontrado</returns>
    /// 
    public XmlNode GetNode(string node, string attr)
    {
        XmlNodeList nodes = xmlroot.SelectNodes(node + "[@" + attr + "]");
        return nodes[0];
    }
    /// <summary>
    /// Metodo que retorna uma lista com os resultados de nodes com nome e atributo especificos
    /// </summary>
    /// <param name="node">Nome do Node</param>
    /// <param name="attr">Nome do Atributo</param>
    /// <returns>Os valores correspondente a todos os nodes encontrados</returns>
    /// 
    public XmlNodeList GetNodes(string node, string attr)
    {
        XmlNodeList nodes = xmlroot.SelectNodes(node + "[@" + attr + "]");
        return nodes;
    }
    /// <summary>
    /// Metodo que retorna o primeiro resultado com nome, atributo e valor de atributo especifico
    /// </summary>
    /// <param name="node">Nome do Node</param>
    /// <param name="attr">Nome do Atributo</param>
    /// <param name="value">Valor do Atributo</param>
    /// <returns>Os valores do primeiro node encontrado</returns>
    /// 
    public XmlNode GetNode(string node, string attr, int value)
    {
        XmlNodeList nodes = xmlroot.SelectNodes(node + "[@" + attr + "='" + value + "']");
        return nodes[0];
    }
    /// <summary>
    /// Metodo que retorna todos os resultados com nome, atributo e valor de atributo especifico
    /// </summary>
    /// <param name="node">Nome do Node</param>
    /// <param name="attr">Nome do Atributo</param>
    /// <param name="value">Valor do Atributo</param>
    /// <returns>Uma lista com os valores encontrados</returns>
    /// 
    public XmlNodeList GetNodes(string node, string attr, int value)
    {
        XmlNodeList nodes = xmlroot.SelectNodes(node + "[@" + attr + "='" + value + "']");
        return nodes;
    }
    /// <summary>
    /// Metodo especifico somente usado se o SetAttributeId e o SetNodeName for definido
    /// </summary>
    /// <param name="idde"> Valor para encontrar no SetAttributeId</param>
    /// <returns>O primeiro resultado com o node encontrado</returns>
    /// 
    public XmlNode GetNode(int idde)
    {
        XmlNodeList nodes = xmlroot.SelectNodes(nodeName + "[@" + attributeId + "='" + idde + "']");
        return nodes[0];
    }
    /// <summary>
    /// Metodo especifico somente usado se o SetAttributeId e o SetNodeName for definido
    /// </summary>
    /// <param name="idde">Valor para encontrar no SetAttributeId</param>
    /// <returns>a lista de nodes encontrado</returns>
    /// 
    public XmlNodeList GetNodes(int idde)
    {
        XmlNodeList nodes = xmlroot.SelectNodes(nodeName + "[@" + attributeId + "='" + idde + "']");
        return nodes;
    }
    /// <summary>
    /// Metodo para facilitar o retorno em texto do conteudo de um node
    /// </summary>
    /// <param name="getNode">Uma lista de nodes</param>
    /// <param name="node">Nome do node para pegar o conteudo</param>
    /// <returns>uma string com o conteudo</returns>
    /// 
    public string GetNodeValue(XmlNode getNode, string node)
    {
        return getNode[node].InnerText;
    }
    /// <summary>
    /// Metodo para facilitar o retorno em texto do conteudo de um node atraves do SetAttributeId
    /// </summary>
    /// <param name="idde">Valor do id a ser retornado</param>
    /// <param name="node">Nome do node</param>
    /// <returns>uma string com o valor do node especificado</returns>
    public string GetNodeValue(int idde, string node)
    {
        return GetNode(idde)[node].InnerText;
    }
}
