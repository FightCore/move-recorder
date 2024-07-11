@echo off

rem Set the path to the AlphaSolve scripts folder
set "alpha_solve_scripts_folder=E:\Tools\AlphaSolveVideo-main\Scripts"

set "top_folder=C:\tmp\recorder"

for /d %%c in ("%top_folder%\*") do (
    echo Processing directory: %%c
    for /d %%m in ("%%c\*") do (
        echo Processing directory: %%m
        cd /d "%%m"
        rem Convert PNGs to AVI using FFmpeg
        ffmpeg -framerate 24 -i "%%03d.png" -b:v 10M output.avi
        rem Execute SolveToAVI.bat script with the output AVI file as argument
        call "%alpha_solve_scripts_folder%\SolveToMKV.bat" output.avi
        call "%alpha_solve_scripts_folder%\SolveToGIF.bat" output.avi
    )
)

