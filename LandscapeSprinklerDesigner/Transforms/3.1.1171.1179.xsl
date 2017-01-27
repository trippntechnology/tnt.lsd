<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes" omit-xml-declaration="no"/>

	<!-- identity template -->
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="TNTCADState">
		<xsl:copy>
			<xsl:apply-templates select="Version"/>
			<Settings xsi:type="SSCSettings">
				<xsl:apply-templates select="*[not(self::ObjectLayers or self::Version)]"/>
			</Settings>
			<xsl:apply-templates select="ObjectLayers"/>
		</xsl:copy>
	</xsl:template>

</xsl:stylesheet>