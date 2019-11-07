# dRAW

## About
Easily search and remove orphan RAW files that don't have corresponding / matching JPG in same directory, and vice versa.

![](dRAW/ressources/pictures/demo_1.PNG)

![](dRAW/ressources/pictures/demo_2.PNG)

## Features
- Delete / move to recycle bin / archive :
  - RAW files with missing JPG
    - ...and vice versa
  - If paired / matching file exists
	- ...or not
- Search recursively (or not) inside a directory
- `Dry-run` mode before deleting / moving files
- Report / log for each deleted / moved file
- Fully portable, no rights / setup needed
- No administrator rights needed
- Free, copyleft license

## Supported file formats :
  - 3FR (Hasselblad)
  - ARI (Arri_Alexa)
  - ARW (Sony)
  - BAY (Casio)
  - BRAW (Blackmagic Design)
  - BMP (Windows bitmap)
  - CAP (Phase One)
  - CR2 (Canon)
  - CR3 (Canon)
  - CRI (Cintel)
  - CRW (Canon)
  - DC2 (Kodak)
  - DCR (Kodak)
  - DCS (Kodak)
  - DNG (Generic)
  - DRF (Kodak)
  - EIP (Phase One)
  - ERF (Epson)
  - FFF (Imacon, Hasselblad)
  - GIF (Graphics Interchange Format)
  - GPR (GoPro)
  - IIQ (Phase One)
  - JPG (Joint Photographic Experts Group)
  - JPEG (Joint Photographic Experts Group)
  - K25 (Kodak)
  - KC2 (Kodak)
  - KDC (Kodak)
  - MDC (Minolta, Agfa)
  - MEF (Mamiya)
  - MOS (Leaf)
  - MRW (Minolta)
  - NEF (Nikon)
  - NRW (Nikon)
  - ORF (Olympus)
  - PDF (Portable Document Format)
  - PEF (Pentax, Samsung)
  - PNG (Portable Network Graphics)
  - PSD (Adobe PhotoShop Document)
  - PTX (Pentax)
  - PXN (Logitech)
  - QTK (Apple)
  - R3D (RED Digital Cinema)
  - RAF (Fuji)
  - RAW (Generic)
  - RDC (Rollei)
  - RW2 (Panasonic)
  - RWL (Leica)
  - RWZ (Rawzor)
  - SR2 (Sony)
  - SRF (Sony)
  - SRW (Samsung)
  - STI (Sinar)
  - TIF (Tagged Image File Format)
  - TIFF (Tagged Image File Format)
  - X3F (Sigma)

## Advanced
To add more supported file formats :
1. Edit `dRAW.exe.config` file :
```xml
...
<setting name="FileFormats" serializeAs="Xml">
	<value>
		<ArrayOfString xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
			xmlns:xsd="http://www.w3.org/2001/XMLSchema">
			<string>3FR (Hasselblad)</string>
			<string>ARI (Arri_Alexa)</string>
			<string>ARW (Sony)</string>
			<string>BAY (Casio)</string>
			<string>BRAW (Blackmagic Design)</string>
			<string>BMP (Windows bitmap)</string>
			...
		</ArrayOfString>
	</value>
</setting>
...
```
3. Save, restart `dRAW`, and enjoy
2. Create a pull request :wink:

## Requirements
- Microsoft [.NET Framework 4](https://www.microsoft.com/en-US/download/details.aspx?id=17851)
- Microsoft Windows Vista or later

## Todo
- Localization
- Multi-threading
- More supported file formats

## Libraries
- Camera emoji :camera: from [Twemoji](https://github.com/twitter/twemoji)

## License
dRAW is released under the [GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.fr.html).
