Hey!
I hope you like this free sample of one of my assets. If you do, some feedback would help me greatly, which you easily can do by rating the model on my site. Thank you!

Some notes regarding texturing:
If you're not using a real time engine, I recommend turning off shadow casting in the glass object properties, to prevent darkness inside (salt/pepper look almost black when dealing with low sample count), or adjusting your material nodes to override given textures with a transparent shader in the case of indirect lighting - all shadow rays from the glass will render as transparent, thus making the object inside bright.

If your software uses glossiness as an input, simply invert the given ROUGHNESS map, which should give you the desired result.