@echo off
for %%f in (*.fx) do (
    mgfxc "%%~nf.fx" "%%~nf.mgfxo" /Profile:OpenGL
)
pause