/** Notice * This file contains works from many authors under various (but compatible) licenses. Please see core.txt for more information. **/
(function(){(window.wpCoreControlsBundle=window.wpCoreControlsBundle||[]).push([[10],{385:function(ia,da,f){f.r(da);var ba=f(1),ea=f(2),aa=f(138);ia=f(88);var ha=f(233);f=f(351);var ca=window;ia=function(f){function w(w,r,h){r=f.call(this,w,r,h)||this;if(w.name&&"xod"!==w.name.toLowerCase().split(".").pop())throw Error("Not an XOD file");if(!ca.FileReader||!ca.File||!ca.Blob)throw Error("File API is not supported in this browser");r.file=w;r.vz=[];r.GF=0;return r}Object(ba.c)(w,f);w.prototype.uI=function(f,r,
h){var n=this,e=new FileReader;e.onloadend=function(f){if(0<n.vz.length){var w=n.vz.shift();w.Ica.readAsBinaryString(w.file)}else n.GF--;if(e.error){f=e.error;if(f.code===f.ABORT_ERR){Object(ea.i)("Request for chunk "+r.start+"-"+r.stop+" was aborted");return}return h(f)}if(f=e.content||f.target.result)return h(!1,f);Object(ea.i)("No data was returned from FileReader.")};r&&(f=(f.slice||f.webkitSlice||f.mozSlice||f.Yja).call(f,r.start,r.stop));0===n.vz.length&&50>n.GF?(e.readAsBinaryString(f),n.GF++):
n.vz.push({Ica:e,file:f});return function(){e.abort()}};w.prototype.Tr=function(f){var r=this;r.qz=!0;var h=aa.a;r.uI(r.file,{start:-h,stop:r.file.size},function(n,e){if(n)return Object(ea.i)("Error loading end header: %s "+n),f(n);if(e.length!==h)throw Error("Zip end header data is wrong size!");r.Jd=new ha.a(e);var w=r.Jd.LQ();r.uI(r.file,w,function(e,h){if(e)return Object(ea.i)("Error loading central directory: %s "+e),f(e);if(h.length!==w.stop-w.start)throw Error("Zip central directory data is wrong size!");
r.Jd.pU(h);r.sF=!0;r.qz=!1;return f(!1)})})};w.prototype.pJ=function(f,r){var h=this,n=h.uh[f];if(h.Jd.jP(f)){var e=h.Jd.Wu(f),w=h.uI(h.file,e,function(n,w){delete h.uh[f];if(n)return Object(ea.i)('Error loading part "%s": %s, '+f+", "+n),r(n);if(w.length!==e.stop-e.start)throw Error("Part data is wrong size!");r(!1,f,w,h.Jd.eS(f))});n.qW=!0;n.cancel=w}else r(Error('File not found: "'+f+'"'),f)};return w}(ia.a);Object(f.a)(ia);Object(f.b)(ia);da["default"]=ia}}]);}).call(this || window)