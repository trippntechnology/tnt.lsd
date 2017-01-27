<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" >

	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes" omit-xml-declaration="no"/>

	<!-- identity template -->
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="DrawingMode">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
			<Layer>
				<xsl:value-of select="../Layer"/>
			</Layer>
			<xsl:if test="../PalettePart">
				<DefaultObject>
					<xsl:attribute name="xsi:type">
						<xsl:value-of select="../PalettePart/@xsi:type"/>
					</xsl:attribute>
					<xsl:copy-of select="../PalettePart/node()"/>
				</DefaultObject>
			</xsl:if>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>