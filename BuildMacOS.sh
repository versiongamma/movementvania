#!/bin/bash

UNITY="../Unity Builds/2020.3.15f2/Unity.app/Contents/MacOS/Unity" # Change this path to compile with different Unity binary
PROJ_DIR=.
PRODUCT_NAME='Movementvania'
MACOS_PROJ='../Build'
IDENTIFIER="com.matty.Movementvania"

cd "${PROJ_DIR}"

if [ -d "${MACOS_PROJ}" ]
then
	rm -fr "${MACOS_PROJ}"
fi

if [ ! -f "${UNITY}" ]
then
	echo "[!] Cannot find Unity install! Please update this build script's Unity location!"
	exit
fi

mkdir "${MACOS_PROJ}"

echo "Starting Unity Build..."
"${UNITY}" -batchmode -quit -projectPath "${PWD}" -executeMethod ExportTool.ExportXcodeProject -logFile "${MACOS_PROJ}/export.log"
if [ ! -d "${MACOS_PROJ}/Movementvania/Movementvania.xcodeproj" ]
then
	echo "[!] Exporting unity project to Xcode failed!"
	exit
fi
echo "Successfully Built Unity Project!"
echo "Starting Xcode Build..."
# Update PRODUCT_NAME
sed -i '' "s/PRODUCT_NAME\ =\ ProductName/PRODUCT_NAME\ =\ \"${PRODUCT_NAME}\"/g" "${MACOS_PROJ}/Movementvania/Movementvania.xcodeproj/project.pbxproj"
sed -i '' "s/com.DefaultCompany.${PRODUCT_NAME}/${IDENTIFIER}/g" "${MACOS_PROJ}/Movementvania/Movementvania.xcodeproj/project.pbxproj"
sed -i '' "s/com.DefaultCompany.${PRODUCT_NAME}/${IDENTIFIER}/g" "${MACOS_PROJ}/Movementvania/Movementvania/Info.plist"

# CODE_SIGN_IDENTITY="iPhone Developer" 
xcodebuild -project "${MACOS_PROJ}/Movementvania/Movementvania.xcodeproj" -scheme Movementvania clean archive -configuration release -archivePath ${MACOS_PROJ}/Movementvania.xcarchive
codesign --force --options=runtime --sign - ${MACOS_PROJ}/Movementvania.xcarchive/Products/Applications/Movementvania.app/Contents/MacOS/Movementvania
if [ ! -d "${MACOS_PROJ}/Movementvania.xcarchive" ]
then
	echo "[!] Exporting Xcode project to xcarchive failed!"
	exit
fi
echo "Build Complete! You can now upload the xcarchive to Apple for notorization."