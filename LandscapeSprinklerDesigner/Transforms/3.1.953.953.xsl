<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes" omit-xml-declaration="no"/>

	<!-- identity template -->
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="/TNTCADState/ObjectLayers/ArrayOfTNTObject/TNTObject[ModelCode='VBSTANDARD']">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
			<MaximumValveQuantity>3</MaximumValveQuantity>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="/TNTCADState/ObjectLayers/ArrayOfTNTObject/TNTObject[ModelCode='VBJUMBO']">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
			<MaximumValveQuantity>4</MaximumValveQuantity>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="/TNTCADState/ObjectLayers/ArrayOfTNTObject/TNTObject[ModelCode='VB10RD']">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
			<MaximumValveQuantity>1</MaximumValveQuantity>
		</xsl:copy>
	</xsl:template>

</xsl:stylesheet>